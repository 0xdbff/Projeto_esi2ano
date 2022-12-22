using System.Diagnostics;

using static Utils.Logger;
using static Utils.File;
using static Utils.Security;

namespace Host;

/// <summary>
///
/// </summary>
internal sealed class Invoice : Payment
{
    /// <summary>
    ///
    /// </summary>
    private bool includeNif = true;

    /// <summary>
    ///
    /// </summary>
    public DateOnly Month { get; private set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="cc"></param>
    private Invoice(PaymentType type, double amount, CreditCard? cc)
        : base(type, amount, cc) { }

    /// <summary>
    ///
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="cc"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    public static async Task<Invoice?> GetAsync(PaymentType type, double amount,
                                                CreditCard? cc, DateOnly month)
    {
        var invoice = new Invoice(type, amount, cc);
        invoice.Month = month;

        await invoice.PaymentAsync(type, amount, cc);

        if (invoice.Status == PaymentStatus.Expired)
            return null;

        return invoice;
    }

    #region pdf_generation

    //! TODO change this to an absolute path on your own server
    /// <summary>
    ///     A path to invoice's latex source code on the host server to generate
    ///     automated invoices on payment.
    /// </summary>
    private static string invoiceSrcPath = "/home/db/dev/repo_g06/Invoice";

    private static void CleanUp()
    {
        try
        {
            using var cleanScript = new Process();

            // Unix style.
            cleanScript.StartInfo.FileName = $"{invoiceSrcPath}/src/cleanScript.sh";
            cleanScript.StartInfo.CreateNoWindow = true;
            cleanScript.StartInfo.RedirectStandardOutput = true;
            cleanScript.Start();
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static async Task GenerateInvoicePdf(Client client)
    {
        //! TODO make it async.
        try
        {
            string texSrc = await GenInvoiceSrc(client);

            using var pdfGen = new Process();

            // Unix style.
            pdfGen.StartInfo.FileName = "pdflatex";
            pdfGen.StartInfo.Arguments =
                $"-output-directory={invoiceSrcPath}/December_2022 {texSrc}";
            pdfGen.StartInfo.CreateNoWindow = true;
            pdfGen.StartInfo.RedirectStandardOutput = true;
            pdfGen.Start();

            await pdfGen.WaitForExitAsync();

            CleanUp();
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<string> GenInvoiceSrc(Client client)
    {
        if (client.subscription == null)
            throw new Exception("Invalid client");

        var address = $@"NIF-{client.Nif}\\{client.Addresses[0].ToStringLatex()}";
        var name = client.Name;
        var email = "{" + client.Email + "}" + "{" + client.Email + "}";
        var month = DateTime.Now.ToString("Y");

        // !TODO change
        var invoiceNumber = (new Random()).Next(0, 999999).ToString();

        string taxRate;
        string product;
        string price;
        string qty = 1.ToString();

        if (client.ClientType == ClientType.Academic)
        {
            taxRate = (0).ToString();

            price = (client.subscription.Type == SubscriptionPlan.Standart)
                        ? (8.22).ToString()
                        : (16.86).ToString();
        }
        else if (client.ClientType == ClientType.Common)
        {
            taxRate = (23).ToString();

            price = (client.subscription.Type == SubscriptionPlan.Standart)
                        ? (8.22 * 1.8).ToString()
                        : (16.86 * 1.8).ToString();
        }
        else
            throw new Exception("Invalid client");

        product =
            $"Subscription IpcaGym {client.subscription.Type} ({client.ClientType}), {month}";

        //! TODO optimize.
        string invoiceSrc;
        invoiceSrc = texSrc.Replace("TAXRATE", taxRate);
        invoiceSrc = invoiceSrc.Replace("INVOICENUMBER", invoiceNumber);
        invoiceSrc = invoiceSrc.Replace("NAME", name);
        invoiceSrc = invoiceSrc.Replace("ADDRESS", address);
        invoiceSrc = invoiceSrc.Replace("EMAIL", email);
        invoiceSrc = invoiceSrc.Replace("PRODUCT", product);
        invoiceSrc = invoiceSrc.Replace("QTY", qty);
        invoiceSrc = invoiceSrc.Replace("PRICE", price);

        month = month.Replace(' ', '_');

        Console.WriteLine(product + " " + price);

        string dir = $"{invoiceSrcPath}/{month}";

        Directory.CreateDirectory(dir);
        dir += $"/{SHA512(DateTime.Now.ToString() + client.Nif)}.tex";

        Console.WriteLine(dir);

        // await CopyFileAsync(invoiceSrcPath + "/invoice.cls", dir +
        // "/invoice.cls");
        await WriteTextAsync(dir, invoiceSrc);

        return dir;
    }

    #region texCode

    /// <summary>
    ///     Preloaded latex src code to improve performance.
    ///     Better cpu performance, worse on binary size.
    /// </summary>
    private static readonly string texSrc =
        @"% Attribution and Licence Notice [http://www.latextemplates.com/template/minimal-invoice]
% [https://creativecommons.org/licenses/by-nc-sa/4.0/]
\documentclass[
        a4paper,
        9pt,
]{/home/db/dev/repo_g06/Invoice/src/invoice}
\taxrate{TAXRATE}
\currencycode{EUR}
\invoicenumber{INVOICENUMBER}
\roundcurrencytodecimals{2}
\roundquantitytodecimals{2}
\sisetup{group-minimum-digits=4}
\sisetup{group-separator={,}}
\sisetup{output-decimal-marker={.}}
\currencysuffix{}
\begin{document}
\setstretch{1.2}
\outputheader{Invoice}{\today}
\outputinvoicenum \\
\begin{minipage}[t]{0.38\textwidth}
        \textbf{Due:} \duedatedays{30}\\
    \textbf{Project:} IpcaGym (Academic purposes)\\
        \textbf{Description:} IpcaGym monthly subscription\\
\end{minipage}
\begin{minipage}[t]{0.03\textwidth}
        ~
\end{minipage}
\begin{minipage}[t]{0.56\textwidth}
	\textbf{NAME}\\
    ADDRESS
	\hrefEMAIL
\end{minipage}
\setstretch{1}
\vfill
\begin{invoicetable}
    \invoiceitem{PRODUCT}{QTY}{PRICE}{}
\end{invoicetable}
\vfill\vfill
\invoiceconditions{Service provided by Instituto Politécnico do Cávado e Ave (Academic work).}
\begin{minipage}[t]{0.3\textwidth}
        \itshape
        \textbf{Address:}\\
    Campus do IPCA - Lugar do Aldão,\\
    4750-810 Vila Frescainha\\
    (São Martinho)\\
    Barcelos, Portugal.
\end{minipage}
\begin{minipage}[t]{0.03\textwidth}
        ~
\end{minipage}
\begin{minipage}[t]{0.3\textwidth}
        \itshape
        \textbf{Contact:}\\
        \href{https://est.ipca.pt/}{https://est.ipca.pt/}\\
    \href{support@ipcagym.org}{support@ipcagym.org} \\
\end{minipage}
\begin{minipage}[t]{0.03\textwidth}
        ~
\end{minipage}
\begin{minipage}[t]{0.3\textwidth}
        \itshape
        \textbf{Payment:}\\
        Bank of Portugal \\
        Sort Code: 010-110 \\
        Account: 10011001110 \\
\end{minipage}
\end{document}";

    #endregion

    #endregion
}

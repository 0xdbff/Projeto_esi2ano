--
-- PostgreSQL database dump
--

-- Dumped from database version 14.6 (Ubuntu 14.6-1.pgdg22.04+1)
-- Dumped by pg_dump version 14.6 (Ubuntu 14.6-1.pgdg22.04+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: address; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.address (
    code uuid NOT NULL,
    postalcode character varying(255) NOT NULL,
    country character varying(255) NOT NULL,
    city character varying(255) NOT NULL,
    lastupdatedate timestamp with time zone,
    aditionalinfo character varying(255),
    housenum integer NOT NULL,
    localidade character varying(255) NOT NULL,
    username character varying(255) NOT NULL
);


ALTER TABLE public.address OWNER TO postgres;

--
-- Name: admin; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.admin (
    id uuid NOT NULL,
    logindatausername character varying(255) NOT NULL,
    gymcode uuid NOT NULL
);


ALTER TABLE public.admin OWNER TO postgres;

--
-- Name: client; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.client (
    id uuid NOT NULL,
    logindatausername character varying(255) NOT NULL,
    gymcode uuid NOT NULL
);


ALTER TABLE public.client OWNER TO postgres;

--
-- Name: creditcard; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.creditcard (
    ccnum integer NOT NULL,
    clientid uuid NOT NULL,
    expirydate timestamp with time zone NOT NULL,
    inserteddate timestamp with time zone NOT NULL,
    securitycode integer NOT NULL,
    nameincc character varying(255) NOT NULL
);


ALTER TABLE public.creditcard OWNER TO postgres;

--
-- Name: creditcard_ccnum_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.creditcard_ccnum_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.creditcard_ccnum_seq OWNER TO postgres;

--
-- Name: creditcard_ccnum_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.creditcard_ccnum_seq OWNED BY public.creditcard.ccnum;


--
-- Name: devicelogin; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.devicelogin (
    globalip character varying(255) NOT NULL,
    logeddate timestamp with time zone NOT NULL,
    logindatausername character varying(255) NOT NULL,
    browser character varying(255) NOT NULL,
    os character varying(255) NOT NULL,
    iplocationlat real NOT NULL,
    iplocationlng real NOT NULL
);


ALTER TABLE public.devicelogin OWNER TO postgres;

--
-- Name: emailinfo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.emailinfo (
    email character varying(255) NOT NULL,
    logindatausername character varying(255) NOT NULL,
    validated integer NOT NULL,
    emailtypetype integer NOT NULL,
    subscrivedtonews boolean
);


ALTER TABLE public.emailinfo OWNER TO postgres;

--
-- Name: emailtype; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.emailtype (
    type integer NOT NULL,
    description character varying(255) NOT NULL
);


ALTER TABLE public.emailtype OWNER TO postgres;

--
-- Name: emailtype_type_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.emailtype_type_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.emailtype_type_seq OWNER TO postgres;

--
-- Name: emailtype_type_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.emailtype_type_seq OWNED BY public.emailtype.type;


--
-- Name: event; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.event (
    code uuid NOT NULL,
    clientid uuid NOT NULL,
    trainerid uuid,
    eventtype integer NOT NULL,
    exteriorspaceid uuid,
    gymcode uuid NOT NULL,
    startdate timestamp with time zone NOT NULL,
    enddate timestamp with time zone NOT NULL,
    comments character varying(255)
);


ALTER TABLE public.event OWNER TO postgres;

--
-- Name: eventtype; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.eventtype (
    type integer NOT NULL,
    description character varying(255) NOT NULL
);


ALTER TABLE public.eventtype OWNER TO postgres;

--
-- Name: eventtype_type_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.eventtype_type_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.eventtype_type_seq OWNER TO postgres;

--
-- Name: eventtype_type_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.eventtype_type_seq OWNED BY public.eventtype.type;


--
-- Name: exteriorspace; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.exteriorspace (
    spaceid uuid NOT NULL,
    clientcapacity integer NOT NULL,
    currentnofclients integer NOT NULL,
    area double precision NOT NULL,
    exteriortype integer NOT NULL,
    gymcode uuid NOT NULL
);


ALTER TABLE public.exteriorspace OWNER TO postgres;

--
-- Name: exteriorspacetype; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.exteriorspacetype (
    type integer NOT NULL,
    description character varying(255)
);


ALTER TABLE public.exteriorspacetype OWNER TO postgres;

--
-- Name: exteriorspacetype_type_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.exteriorspacetype_type_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.exteriorspacetype_type_seq OWNER TO postgres;

--
-- Name: exteriorspacetype_type_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.exteriorspacetype_type_seq OWNED BY public.exteriorspacetype.type;


--
-- Name: gym; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.gym (
    code uuid NOT NULL,
    clientcapacity integer NOT NULL,
    currentnofclients integer NOT NULL,
    nif integer,
    mbentiity character varying(255),
    addresscode uuid NOT NULL
);


ALTER TABLE public.gym OWNER TO postgres;

--
-- Name: invoice; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.invoice (
    paymentchecknum uuid NOT NULL,
    tax integer,
    email boolean,
    includenif boolean
);


ALTER TABLE public.invoice OWNER TO postgres;

--
-- Name: logindata; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.logindata (
    username character varying(255) NOT NULL,
    hashedpassword character varying(255) NOT NULL,
    twofactorauthapp character varying(255),
    lastlogin timestamp with time zone,
    usertype integer NOT NULL
);


ALTER TABLE public.logindata OWNER TO postgres;

--
-- Name: monthlyfinancialanalysis; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.monthlyfinancialanalysis (
    month date NOT NULL,
    expenses double precision NOT NULL,
    income double precision NOT NULL,
    gymcode uuid NOT NULL
);


ALTER TABLE public.monthlyfinancialanalysis OWNER TO postgres;

--
-- Name: payment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.payment (
    checknum uuid NOT NULL,
    clientid uuid NOT NULL,
    gymcode uuid NOT NULL,
    paiddate timestamp with time zone,
    amount double precision NOT NULL,
    expirydate timestamp with time zone NOT NULL,
    paymentinfo character varying(255) NOT NULL,
    status character varying(255) NOT NULL,
    refmb integer
);


ALTER TABLE public.payment OWNER TO postgres;

--
-- Name: paymenttype; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.paymenttype (
    type integer NOT NULL,
    paymentchecknum uuid NOT NULL,
    description character varying(255) NOT NULL
);


ALTER TABLE public.paymenttype OWNER TO postgres;

--
-- Name: paymenttype_type_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.paymenttype_type_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.paymenttype_type_seq OWNER TO postgres;

--
-- Name: paymenttype_type_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.paymenttype_type_seq OWNED BY public.paymenttype.type;


--
-- Name: physicalconditionbymonth; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.physicalconditionbymonth (
    clientid uuid NOT NULL,
    month date NOT NULL,
    height double precision,
    weight double precision,
    imc double precision
);


ALTER TABLE public.physicalconditionbymonth OWNER TO postgres;

--
-- Name: refmb; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.refmb (
    reference integer NOT NULL,
    refexpirydate integer NOT NULL
);


ALTER TABLE public.refmb OWNER TO postgres;

--
-- Name: refmb_reference_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.refmb_reference_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.refmb_reference_seq OWNER TO postgres;

--
-- Name: refmb_reference_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.refmb_reference_seq OWNED BY public.refmb.reference;


--
-- Name: statisticsbymonth; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.statisticsbymonth (
    clientid uuid NOT NULL,
    month date NOT NULL,
    runneddistance double precision,
    estimatedlostcalories double precision,
    besttimetorunamile double precision,
    averagehearthratebeats double precision,
    averageoverallperformance double precision
);


ALTER TABLE public.statisticsbymonth OWNER TO postgres;

--
-- Name: subscription; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.subscription (
    clientid uuid NOT NULL,
    subscriptiontype integer NOT NULL,
    requiredpaymentdate timestamp with time zone NOT NULL,
    startdate timestamp with time zone NOT NULL,
    status integer NOT NULL,
    automaticrenewal boolean NOT NULL,
    comments character varying(255),
    nofcancelations integer NOT NULL
);


ALTER TABLE public.subscription OWNER TO postgres;

--
-- Name: subscriptiontype; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.subscriptiontype (
    type integer NOT NULL,
    description character varying(255) NOT NULL,
    normalprice double precision NOT NULL
);


ALTER TABLE public.subscriptiontype OWNER TO postgres;

--
-- Name: subscriptiontype_type_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.subscriptiontype_type_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.subscriptiontype_type_seq OWNER TO postgres;

--
-- Name: subscriptiontype_type_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.subscriptiontype_type_seq OWNED BY public.subscriptiontype.type;


--
-- Name: trainer; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.trainer (
    id uuid NOT NULL,
    logindatausername character varying(255) NOT NULL,
    gymcode uuid NOT NULL,
    rating double precision
);


ALTER TABLE public.trainer OWNER TO postgres;

--
-- Name: userdata; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.userdata (
    logindatausername character varying(255) NOT NULL,
    firstname character varying(255) NOT NULL,
    lastname character varying(255) NOT NULL,
    birthdate timestamp with time zone NOT NULL,
    gender character varying(255) NOT NULL,
    nif integer NOT NULL,
    phone integer NOT NULL,
    usersince timestamp with time zone NOT NULL,
    lastpasswreset timestamp with time zone
);


ALTER TABLE public.userdata OWNER TO postgres;

--
-- Name: creditcard ccnum; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.creditcard ALTER COLUMN ccnum SET DEFAULT nextval('public.creditcard_ccnum_seq'::regclass);


--
-- Name: emailtype type; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.emailtype ALTER COLUMN type SET DEFAULT nextval('public.emailtype_type_seq'::regclass);


--
-- Name: eventtype type; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.eventtype ALTER COLUMN type SET DEFAULT nextval('public.eventtype_type_seq'::regclass);


--
-- Name: exteriorspacetype type; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exteriorspacetype ALTER COLUMN type SET DEFAULT nextval('public.exteriorspacetype_type_seq'::regclass);


--
-- Name: paymenttype type; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paymenttype ALTER COLUMN type SET DEFAULT nextval('public.paymenttype_type_seq'::regclass);


--
-- Name: refmb reference; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refmb ALTER COLUMN reference SET DEFAULT nextval('public.refmb_reference_seq'::regclass);


--
-- Name: subscriptiontype type; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.subscriptiontype ALTER COLUMN type SET DEFAULT nextval('public.subscriptiontype_type_seq'::regclass);


--
-- Data for Name: address; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.address (code, postalcode, country, city, lastupdatedate, aditionalinfo, housenum, localidade, username) FROM stdin;
09ab91c4-74c3-423d-9780-92073414f8c4	4750-810	Portugal	Barcelos	2023-01-03 20:54:58.092309+00	Lugar do Aldão	2	Vila Frescainha (São Martinho)	IpcaGymAdmin
3e059667-aa2a-495f-a469-1c8878029c90	4022-453	Portugal Continental	Braga	2023-01-03 20:54:58.332419+00	Rua Exemplo	2	Ponta do Sol	Aaren
218c184c-2393-46b3-90d2-2336c8509876	4047-891	Portugal Continental	Braga	2023-01-03 20:54:58.397849+00	Rua Exemplo	2	Moimenta da Beira	Aarika
78fb68ac-f05b-43c9-b465-2d654d316b04	4239-406	Portugal Continental	Bragança	2023-01-03 20:54:58.455841+00	Rua Exemplo	2	Penafiel	Aaron
3b46abfe-c3c1-4d35-85a7-d95bb5a73d0c	4182-969	Portugal Continental	Santarém	2023-01-03 20:54:58.506804+00	Rua Exemplo	2	Cuba	Aartjan
1b6cab2b-999e-479e-b2fb-3a0021ebb27e	4106-393	Portugal Continental	Castelo Branco	2023-01-03 20:54:58.556554+00	Rua Exemplo	2	Arcos de Valdevez	Abagael
3fced939-5a5d-4bb2-99df-429c3b21deb6	4063-229	Portugal Continental	Porto	2023-01-03 20:54:58.610748+00	Rua Exemplo	2	Santa Comba Dão	Abagail
ec27c92b-671a-47c4-9d95-bdd29043551a	4943-568	Portugal Continental	Coimbra	2023-01-03 20:54:58.661097+00	Rua Exemplo	2	Carrazeda de Ansiães	Abahri
3385cce2-da8c-46c8-a4db-450d034610e0	4195-564	Portugal Continental	Leiria	2023-01-03 20:54:58.709641+00	Rua Exemplo	2	Ovar	Abbas
544bf885-ae23-481e-8dbb-613c057698a1	4173-368	Portugal Continental	Viseu	2023-01-03 20:54:58.768885+00	Rua Exemplo	2	Vila Verde	Abbe
ce127c61-4b0f-4395-b30e-bd2224fc6ec0	4157-614	Portugal Continental	Santarém	2023-01-03 20:54:58.822482+00	Rua Exemplo	2	Vila Velha de Ródão	Abbey
4f989334-d55e-48ce-8669-08f6b7b16824	4990-402	Portugal Continental	Castelo Branco	2023-01-03 20:54:58.876003+00	Rua Exemplo	2	Nazaré	Abbi
45262844-2389-4287-ac91-37cff15c9448	4420-467	Portugal Continental	Guarda	2023-01-03 20:54:58.929554+00	Rua Exemplo	2	Penalva do Castelo	Abbie
56f79afe-f68a-4772-9e79-41015f73d283	4749-541	Portugal Continental	Viseu	2023-01-03 20:54:59.022725+00	Rua Exemplo	2	Seixal	Abby
66a153b3-147e-40a6-a271-965dd5206ee3	4751-118	Portugal Continental	Setúbal	2023-01-03 20:54:59.086465+00	Rua Exemplo	2	Alcoutim	Abbye
e5c45620-9f61-4047-a300-ebbfcf99f66a	4798-299	Portugal Continental	Beja	2023-01-03 20:54:59.134653+00	Rua Exemplo	2	Paços de Ferreira	Abdalla
5712a2dc-89d1-48eb-b429-ae4fa5b3cbfe	4334-469	Portugal Continental	Faro	2023-01-03 20:54:59.185216+00	Rua Exemplo	2	Mourão	Abdallah
3f0b5764-f7c8-4d00-ba2b-44a780addd93	4585-593	Portugal Continental	Braga	2023-01-03 20:54:59.244603+00	Rua Exemplo	2	Azambuja	Abdul
1864919a-aa50-46f6-a745-6c6a6277895a	4845-971	Portugal Continental	Vila Real	2023-01-03 20:54:59.29634+00	Rua Exemplo	2	Aljezur	Abdullah
cdaefa80-0897-40e2-8528-7585a33afffd	4699-689	Portugal Continental	Évora	2023-01-03 20:54:59.347936+00	Rua Exemplo	2	Alcácer do Sal	Abe
8c36f188-ee90-45f9-96a8-21c7b2cf0e70	4655-225	Portugal Continental	Vila Real	2023-01-03 20:54:59.400676+00	Rua Exemplo	2	Cartaxo	Abel
052dd8f2-3d35-40e5-b1e9-d877f41dce86	4329-382	Portugal Continental	Beja	2023-01-03 20:54:59.456089+00	Rua Exemplo	2	Calheta	Abigael
44753440-ecf7-41e8-b6f0-a07a70d020dd	4481-995	Portugal Continental	Beja	2023-01-03 20:54:59.504002+00	Rua Exemplo	2	Mortágua	Abigail
2ece558f-f5e4-4837-b006-b51676d84826	4303-229	Portugal Continental	Setúbal	2023-01-03 20:54:59.55475+00	Rua Exemplo	2	Chaves	Abigale
03b3ebab-050d-4bb4-aed0-f918bfd658c5	4682-583	Portugal Continental	Setúbal	2023-01-03 20:54:59.616045+00	Rua Exemplo	2	Mogadouro	Abra
d89067f8-9749-4055-a6ad-cd70ca7e6b06	4867-557	Portugal Continental	Beja	2023-01-03 20:54:59.679578+00	Rua Exemplo	2	Ovar	Abraham
c28baa56-8035-4c1b-a0db-d500ae44be4a	4988-538	Portugal Continental	Lisboa	2023-01-03 20:54:59.72875+00	Rua Exemplo	2	Penedono	Abu
f5a0b3a8-8a4d-4071-afd6-4bc0dfb756da	4226-764	Portugal Continental	Lisboa	2023-01-03 20:54:59.782906+00	Rua Exemplo	2	Vieira do Minho	Access
c73cb635-6cc3-43a5-b069-d5f9bed0b1dc	4819-215	Portugal Continental	Viseu	2023-01-03 20:54:59.837447+00	Rua Exemplo	2	Montijo	Accounting
b5989684-4e8f-4b4c-a59b-f3d81c864418	4966-136	Portugal Continental	Lisboa	2023-01-03 20:54:59.896692+00	Rua Exemplo	2	Mira	Achal
428e887a-cf36-4758-bd6a-23127f383036	4256-588	Portugal Continental	Évora	2023-01-03 20:54:59.952392+00	Rua Exemplo	2	Portalegre	Achamma
aa6c1af0-d240-4e1b-a0be-726bf14b9b6c	4050-447	Portugal Continental	Vila Real	2023-01-03 20:55:00.001384+00	Rua Exemplo	2	Trofa	Action
7877d879-7ea4-4289-b191-113862954c7d	4060-359	Portugal Continental	Santarém	2023-01-03 20:55:00.056586+00	Rua Exemplo	2	Ferreira do Zêzere	Ada
25d25d9a-01eb-4dfb-b7b0-90d5a3206fba	4872-652	Portugal Continental	Leiria	2023-01-03 20:55:00.104721+00	Rua Exemplo	2	Angra do Heroísmo	Adah
bb13fa16-b8f7-4d53-81b4-c516b4b50fde	4279-615	Portugal Continental	Setúbal	2023-01-03 20:55:00.15524+00	Rua Exemplo	2	Mesão Frio	Adaline
8659fec6-6bc1-4b49-a8d9-a01824f9b534	4309-688	Portugal Continental	Santarém	2023-01-03 20:55:00.209141+00	Rua Exemplo	2	Castro Daire	Adam
7ad84663-68d8-4173-97f9-29c5e13d27b8	4962-958	Portugal Continental	Beja	2023-01-03 20:55:00.266393+00	Rua Exemplo	2	Alenquer	Adan
44fcd0b5-fdca-40a9-8fd9-87f454120dca	4340-768	Portugal Continental	Santarém	2023-01-03 20:55:00.31521+00	Rua Exemplo	2	Loulé	Adara
cdb15d45-69a2-42bb-b5d8-09f0b0c8217e	4238-753	Portugal Continental	Braga	2023-01-03 20:55:00.363839+00	Rua Exemplo	2	Macedo de Cavaleiros	Adda
7a6c5c19-897c-4c61-ae81-df895b2bcdd9	4317-851	Portugal Continental	Beja	2023-01-03 20:55:00.415771+00	Rua Exemplo	2	Sobral de Monte Agraço	Addi
53dc87e1-1414-4a85-b15d-4a5f559aafeb	4024-939	Portugal Continental	Braga	2023-01-03 20:55:00.46772+00	Rua Exemplo	2	São João da Madeira	Addia
8d16024a-f7c0-44e9-ba39-0ccf6b62cf22	4928-113	Portugal Continental	Portalegre	2023-01-03 20:55:00.516243+00	Rua Exemplo	2	Silves	Addie
849eb2c1-6370-46a5-8ced-a4b635a5bb81	4629-638	Portugal Continental	Évora	2023-01-03 20:55:00.564072+00	Rua Exemplo	2	Castanheira de Pêra	Addons
e8d68129-63b5-4c4b-aa5e-1b2a28bac6ff	4747-673	Portugal Continental	Setúbal	2023-01-03 20:55:00.619758+00	Rua Exemplo	2	Serpa	Addy
b27a00f0-b845-4098-8f1f-ef6e779f856a	4512-306	Portugal Continental	Coimbra	2023-01-03 20:55:00.668579+00	Rua Exemplo	2	Felgueiras	Adel
6a779b2a-5b47-4ef1-a352-ceee74359ea3	4014-122	Portugal Continental	Lisboa	2023-01-03 20:55:00.786049+00	Rua Exemplo	2	Vila Nova de Gaia	Adela
a61eb0ac-b882-4319-b33c-51f04a237317	4636-311	Portugal Continental	Leiria	2023-01-03 20:55:00.855836+00	Rua Exemplo	2	Pombal	Adelaida
a7d25d3f-5724-4394-9eba-2171c065950d	4352-278	Portugal Continental	Braga	2023-01-03 20:55:00.906763+00	Rua Exemplo	2	Gouveia	Adelaide
4a9d77f3-7167-4504-8247-534931055133	4088-877	Portugal Continental	Porto	2023-01-03 20:55:00.963463+00	Rua Exemplo	2	São João da Pesqueira	Adele
c53b97e3-2b22-4409-a2ba-b9fb13bfa06a	4575-607	Portugal Continental	Beja	2023-01-03 20:55:01.062121+00	Rua Exemplo	2	Sátão	Adelheid
52d5449f-de32-440b-96fe-b066f5e601f6	4707-447	Portugal Continental	Faro	2023-01-03 20:55:01.155779+00	Rua Exemplo	2	Figueiró dos Vinhos	Adelia
\.


--
-- Data for Name: admin; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.admin (id, logindatausername, gymcode) FROM stdin;
\.


--
-- Data for Name: client; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.client (id, logindatausername, gymcode) FROM stdin;
\.


--
-- Data for Name: creditcard; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.creditcard (ccnum, clientid, expirydate, inserteddate, securitycode, nameincc) FROM stdin;
\.


--
-- Data for Name: devicelogin; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.devicelogin (globalip, logeddate, logindatausername, browser, os, iplocationlat, iplocationlng) FROM stdin;
\.


--
-- Data for Name: emailinfo; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.emailinfo (email, logindatausername, validated, emailtypetype, subscrivedtonews) FROM stdin;
admin@ipcagym.org	IpcaGymAdmin	1	2	t
alunos.Aaren@ipca.example.pt	Aaren	1	0	t
alunos.Aarika@ipca.example.pt	Aarika	1	0	t
alunos.Aaron@ipca.example.pt	Aaron	1	0	t
Aartjan@gmail.example	Aartjan	1	1	t
alunos.Abagael@ipca.example.pt	Abagael	1	0	t
alunos.Abagail@ipca.example.pt	Abagail	1	0	t
Abahri@gmail.example	Abahri	1	1	t
Abbas@gmail.example	Abbas	1	1	t
alunos.Abbe@ipca.example.pt	Abbe	1	0	t
Abbey@gmail.example	Abbey	1	1	t
alunos.Abbi@ipca.example.pt	Abbi	1	0	t
alunos.Abbie@ipca.example.pt	Abbie	1	0	t
alunos.Abby@ipca.example.pt	Abby	1	0	t
Abbye@gmail.example	Abbye	1	1	t
alunos.Abdalla@ipca.example.pt	Abdalla	1	0	t
Abdallah@gmail.example	Abdallah	1	1	t
alunos.Abdul@ipca.example.pt	Abdul	1	0	t
Abdullah@gmail.example	Abdullah	1	1	t
alunos.Abe@ipca.example.pt	Abe	1	0	t
alunos.Abel@ipca.example.pt	Abel	1	0	t
alunos.Abigael@ipca.example.pt	Abigael	1	0	t
alunos.Abigail@ipca.example.pt	Abigail	1	0	t
alunos.Abigale@ipca.example.pt	Abigale	1	0	t
alunos.Abra@ipca.example.pt	Abra	1	0	t
alunos.Abraham@ipca.example.pt	Abraham	1	0	t
Abu@gmail.example	Abu	1	1	t
alunos.Access@ipca.example.pt	Access	1	0	t
Accounting@gmail.example	Accounting	1	1	t
alunos.Achal@ipca.example.pt	Achal	1	0	t
alunos.Achamma@ipca.example.pt	Achamma	1	0	t
alunos.Action@ipca.example.pt	Action	1	0	t
alunos.Ada@ipca.example.pt	Ada	1	0	t
alunos.Adah@ipca.example.pt	Adah	1	0	t
alunos.Adaline@ipca.example.pt	Adaline	1	0	t
alunos.Adam@ipca.example.pt	Adam	1	0	t
Adan@gmail.example	Adan	1	1	t
alunos.Adara@ipca.example.pt	Adara	1	0	t
alunos.Adda@ipca.example.pt	Adda	1	0	t
alunos.Addi@ipca.example.pt	Addi	1	0	t
alunos.Addia@ipca.example.pt	Addia	1	0	t
alunos.Addie@ipca.example.pt	Addie	1	0	t
alunos.Addons@ipca.example.pt	Addons	1	0	t
alunos.Addy@ipca.example.pt	Addy	1	0	t
alunos.Adel@ipca.example.pt	Adel	1	0	t
alunos.Adela@ipca.example.pt	Adela	1	0	t
Adelaida@gmail.example	Adelaida	1	1	t
alunos.Adelaide@ipca.example.pt	Adelaide	1	0	t
Adele@gmail.example	Adele	1	1	t
alunos.Adelheid@ipca.example.pt	Adelheid	1	0	t
alunos.Adelia@ipca.example.pt	Adelia	1	0	t
\.


--
-- Data for Name: emailtype; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.emailtype (type, description) FROM stdin;
0	academic
1	common
2	gymEmail
\.


--
-- Data for Name: event; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.event (code, clientid, trainerid, eventtype, exteriorspaceid, gymcode, startdate, enddate, comments) FROM stdin;
\.


--
-- Data for Name: eventtype; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.eventtype (type, description) FROM stdin;
\.


--
-- Data for Name: exteriorspace; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.exteriorspace (spaceid, clientcapacity, currentnofclients, area, exteriortype, gymcode) FROM stdin;
\.


--
-- Data for Name: exteriorspacetype; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.exteriorspacetype (type, description) FROM stdin;
\.


--
-- Data for Name: gym; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.gym (code, clientcapacity, currentnofclients, nif, mbentiity, addresscode) FROM stdin;
\.


--
-- Data for Name: invoice; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.invoice (paymentchecknum, tax, email, includenif) FROM stdin;
\.


--
-- Data for Name: logindata; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.logindata (username, hashedpassword, twofactorauthapp, lastlogin, usertype) FROM stdin;
IpcaGymAdmin	35b65c2ce3bec8c091299d0372adc6965aba0828a6d7b82a90eed97412020a2e2352ab0a6fa1a27a2911a7e2f0a74b747890f770de44bfe7557cbc1dd9f36415		2023-01-03 20:54:58.05313+00	0
Aaren	b2c84abff63719fa68eae46988036e80ed0dceebb1d4169e555a0f4295efb41c784e72fd572928cb721fed43553b1cf0538ff63cd5f28e5c9950de3fb1af4f2e	not set	2023-01-03 20:54:58.295216+00	2
Aarika	9ad04ef778df4a633fc01164d6b485ce0c6cbf7cabd0405ec9eb0b37c2f65f295b5a4384a59f1e6e8bc48c65c3e2e457c12013cd91d55d62016b6eebf34f5c87	not set	2023-01-03 20:54:58.357594+00	2
Aaron	28f1bd61dc70723e7c6c4ba116b819513d5cd1c927a16ed8cc2e99da2913238a9e4f8f7974adf5d47830800fb771c9b64d7d6462ee5d506b43e3c97cb4c8f759	not set	2023-01-03 20:54:58.414171+00	2
Aartjan	5c8774faf703a82e37f420846ec08a99889ce241b7322758f53a5db12e4fea2420c54db4766941db0a29140ad7c003b3433dd4d6d667203d740f3d9baef33ca4	not set	2023-01-03 20:54:58.470494+00	2
Abagael	b1db8b683bf3be35360e35d80db84b139d87aa3ffc21ac9ddb60af95fe0c694a30a3ae6b2468c94c3223c13fda8f8b45e2085cde506c51569af3257f5657bc27	not set	2023-01-03 20:54:58.520263+00	2
Abagail	bf66e424e60191c690f2215b757cbb404de1664b16dfbff9b9a41c8213de30ee4c93df95d16cbafc79d97a432dcf07d6518998549b7683291f755702c68c7619	not set	2023-01-03 20:54:58.573776+00	2
Abahri	ce6abd3b1a78741eb95fbb3a660618903a8488ffced36e2f4cdef29fdd5f307353b2da5ae0ee45bea21ceba30e4a8e4b7c7c01ae80c0c2014259d4793fc1a685	not set	2023-01-03 20:54:58.6243+00	2
Abbas	fdd33e298eb78bbc05e19ff8f2728141fdae76f164b81136c273087a6dc3840ecf936fc4e540b6f04f850528ee47053a6588472d6854a7bc293335a064c4c6a4	not set	2023-01-03 20:54:58.674138+00	2
Abbe	e6c83b282aeb2e022844595721cc00bbda47cb24537c1779f9bb84f04039e1676e6ba8573e588da1052510e3aa0a32a9e55879ae22b0c2d62136fc0a3e85f8bb	not set	2023-01-03 20:54:58.722221+00	2
Abbey	83004bb19c3daaf3babbeb0aa831acaf52eca126abe8d74628e22b6ec6a5741dc61680e3fc7497073911a49bf1db94196900dfe49b766aed91781f829a7f2c00	not set	2023-01-03 20:54:58.785087+00	2
Abbi	8b6b8f8bdf8cd4d2ac2a57d78dcd9225fdf02ce95d09804fcf46df8a8b7dbb143d6290cef80d21ac71f4ac220d3276d566227069e8deb500c2be56b2b2b29091	not set	2023-01-03 20:54:58.835908+00	2
Abbie	72cd06bd36df7019dbe93283eddc1774d205d2a52e247000683d69d9f47c56ac65fa5538404829518342c75d7f3175936df906fdafd3e5089c4c1e2f698b4a5a	not set	2023-01-03 20:54:58.891574+00	2
Abby	42cba7f6a84794220382c72fbe96b50ac96ce622484c314ff1816ada3ac6e5d5013505b3f11ca9f4eddbb7eb2dea4eb0b3f964cac5736e627d4efa3e8b3b5228	not set	2023-01-03 20:54:58.944269+00	2
Abbye	a71e2cf7a3235dbea0d1f9a7a11518c674c299ff7cee2b4e8d9fbf531e3d1c5dd9f9ec9162113ecadfa92175a64c523fdbb27f38def2a2b591adb07497fb1fd1	not set	2023-01-03 20:54:59.045313+00	2
Abdalla	625f7fdb99de7de358ab119ead94c29b436764e1bffb3af4f1ca715b692cf155e62007572ce4101fef09a98130369de7a06ccd57903b4c5a9104d1444a02f4a2	not set	2023-01-03 20:54:59.099186+00	2
Abdallah	b6b1266e713a640a4f6d7b8450b54dbd45c2874ea634dfc9bb43d5e50ad6ee047b2df87113cbfa2b3b0ae9091cceb1b24bf8e9599f34b9091438e9cca220fa78	not set	2023-01-03 20:54:59.147258+00	2
Abdul	065bb0b9a0c654e5b3b6292c4698bd67ce6a331209d941989ec4d728fbe3290e47d2058839215bbe6144f51e7fce8a8c6a5626e0cb7521641d742251f5a17167	not set	2023-01-03 20:54:59.202029+00	2
Abdullah	339a01c7d8c1cc8d7bc5e459e9e2a4a062c8cba9f061a71e40c09a70532cd8e74c28be16af4dd64c64e8890c237c50458b9d05c051a45efa8a8098c008207f31	not set	2023-01-03 20:54:59.259637+00	2
Abe	51c0bd94370be34acdee4f3f01cd1a789b3cf5c5a3a42954e34baa3c7d6e2fee6332580b7eeceb02a6914ca2eab8266de1ed1058fb45d07fac31631155dd0924	not set	2023-01-03 20:54:59.311381+00	2
Abel	59a94a0ac0f75200d1477d0f158a23d7feb08a2db16d21233b36fc8fda1a958c1be52b439f7957733bd65950cdfa7918b2f76a480ed01bb6e4edf4614eb8a708	not set	2023-01-03 20:54:59.364164+00	2
Abigael	d3a73f2f7b7c4075cd69c76f9b7199c6d2ba7bf32ed1d27ce2849edb8aa785f3212ab3b11283cb49396eacb583cd24db1ff7e78dc57420dcd5b4825440979ce4	not set	2023-01-03 20:54:59.419219+00	2
Abigail	650dee9bcaa767c7afbf3af367e976f0895f2eb0f1caac475bb6eae75b8874381036716417128945ecedeeba6ec019f9e768ae1e683a9066350f106b6a098611	not set	2023-01-03 20:54:59.469438+00	2
Abigale	5d5e3556b020d364f88a3a8190a6f392989c00fd8482e6d12468549633acd23b149c3a19d225c312d2fcbaad7d0bd7725404152e6af6424159e30c7f073a985d	not set	2023-01-03 20:54:59.517134+00	2
Abra	591f110c5f0e1bdce3ce99ee4d25a60959f343e90d120e4d473e3ccae5811d5d07cd991a64b6f18ef670b772d48adbb9c08206bb99e621b60046b3921b204b77	not set	2023-01-03 20:54:59.571638+00	2
Abraham	b07aadf32af05c844db101658e7e3708efb2f54c05e3c766aa95c1cdada57a14e3571bee17186d843eac9dd04b3e9ff9b9b3e30e888bf4562ff9479caaebe16f	not set	2023-01-03 20:54:59.63758+00	2
Abu	6074f544e2b0c180f852af4e14d70c74a91052eedd5d583bc52e5fbdd90fe731ea347428485fe7ca82c63c382feb2de0539cca2ec65c92b7ead2866c092dae22	not set	2023-01-03 20:54:59.692527+00	2
Access	5c06536cde354bf7cfd399d68d776114dec8e12172fa1da4f38c91f0ee613d147b8d64aa5b32475b8bd64bc28da63087ad0bfee71e588c6bf2babb9a02ebfcd4	not set	2023-01-03 20:54:59.742747+00	2
Accounting	a91d24d7eab7683bc73b857d42dfc48a9577c600ccb5e7d7adabab54eebc112232f3de2539208f22a560ad320d1f2cda5a5f1a127baf6bf871b0e282c2b85220	not set	2023-01-03 20:54:59.797555+00	2
Achal	7a7365f1de2da275b3fb49ee53646b6405e7102f9ce0ee5d4d87c2eebd337ff1c9b60fd225a1d16ceeb97617e4a2c8bd8b7c86f745403508950b7bc059451b7f	not set	2023-01-03 20:54:59.853128+00	2
Achamma	e572ded55766b31e244c57827519c9a1a14be113115f90b7033c639379f8c9393df09fcc3d5fdb8efbbfacc966998b43c57d2dac9c5a066f47fd8295914d7356	not set	2023-01-03 20:54:59.913335+00	2
Action	549ce8323114e9a87bf4654c4362f1a802514e3e83147d1d8db6f57cde98a9ddd528f1713945aa28558142327f14c5d7bce0cffa698e66928cda9f1334a6ef37	not set	2023-01-03 20:54:59.965404+00	2
Ada	4b9e7b9e27f2605875092a0378662063af754afb6d9bc91d40d7652cd12df54657c0c1cb2f5126b5966a92340433a6d21808cd986f65b0bb0846b0a443fcf58e	not set	2023-01-03 20:55:00.019387+00	2
Adah	be544f75277c1c2c2eb1f7192e7104a47671ce6ec625f798964cbbadaca112edb29ef62b2d10ad527d8bf2d07e06285c3f1619026a3b9bedae8a472d265c0d2e	not set	2023-01-03 20:55:00.069595+00	2
Adaline	070bf869119ddef6b166f832661e06df9fb19879a38f09e814d33a3b4501db9fc6ea1d64e345a9185346893805f1bd9aad892fc999d8ab4f5063d6f6b39fca53	not set	2023-01-03 20:55:00.118134+00	2
Adam	a8783749cff88f94fe983f9e4123a3b5848216eb0fa95f0c334abe27fea06c391e376512641c88c0ded61c2b08446a29c38f90a38583b9eb90a42d218ce24c39	not set	2023-01-03 20:55:00.16923+00	2
Adan	b0eef5b5eab20037873a50861cd3d1b24b515d9e7f8caed71451f6f2707cb7bf4637881903cd1914cb1f339b374bd8582afb3e9d7b6fdb285cacf2a63a3d2dfd	not set	2023-01-03 20:55:00.228878+00	2
Adara	f598a9bfdd177102b7585cf27a080f81e848c59e88bd05235a331989fca9dee4f8416b1ac4cd52fdd63b6ae1f369b98a236c1133547dcafcd8144257b2619124	not set	2023-01-03 20:55:00.279447+00	2
Adda	728db48989c9878bdb727058ae0d0968c5902f488dd9e3d4a4aa3f90410da5566fd0ca5f59c6a58154cce2e5c8e7a2586a79d88397d12c46b830ee50890971eb	not set	2023-01-03 20:55:00.328071+00	2
Addi	2d3280e1b732ad4728a3109c83e0c72ca853f6409bb46a6c750e38868da4c9bba9f5b2b255f2576ff27ccdbd1b969d7ce89ae187515d553ab7dad9279112205d	not set	2023-01-03 20:55:00.376593+00	2
Addia	d3a73f2f7b7c4075cd69c76f9b7199c6d2ba7bf32ed1d27ce2849edb8aa785f3212ab3b11283cb49396eacb583cd24db1ff7e78dc57420dcd5b4825440979ce4	not set	2023-01-03 20:55:00.432297+00	2
Addie	4f91d9fc630bde56be9cf10b005f765bf73924d7e1b984e7fffd4fc3fa371de3a7f1b594f978dba37c8c246e8d4ceef63f5e73105fef76cb3b6e7a8a2678a233	not set	2023-01-03 20:55:00.480445+00	2
Addons	cafb9343520b40dc18fa0df9dc81b618bb2c524f66e40600c1d530ddb1e434ef6a5c3d4573fa019f5302e18b9653c4e73d599167c979b1b7259d9292451c0fd2	not set	2023-01-03 20:55:00.528563+00	2
Addy	d18ba9657eefc68c49e69664b507fddeb1698d7b02cf49528cdf6fd63eee7a49d3385811eec45a6a2b6d23efe5ee8a4d544d034c27d8808ff3798effa9959343	not set	2023-01-03 20:55:00.578402+00	2
Adel	81da262dd4b4d531576925ab45dcdaad7f8d46c668b2bbbde414939402f01fe16223fc872a179a21bcfbc526ca43a7a972cb89befec69287bd5323a88c650957	not set	2023-01-03 20:55:00.632645+00	2
Adela	f39249cecce22aeff3112526513d97ee8724a45063dd37b7a872ba68d56cc89aef303c9c0a55888613a93048b5799a4115e4f055792ae9a29564026bd9a88482	not set	2023-01-03 20:55:00.711998+00	2
Adelaida	756ba19c9dc9fb00e474b17d50f2969093239e88f90a154b1bb8d258a566a7a57a643891b96319d8e741b0566f18cc485d1c7b68e186f71682eb52d25b8cfef2	not set	2023-01-03 20:55:00.816569+00	2
Adelaide	fbe3d0c76a01499b44902c726b7618aca7d7a082b0895f5c32dc60699032c79e56ccb771437c40be54ed22ca742b765e8e75beafe0bfc2a517851c410dc6ad48	not set	2023-01-03 20:55:00.868854+00	2
Adele	ab6a4b60a4618419595c6d9b5cad8de7be93c8eb6d304f564ca2badfd2df6e1f62d2dbedab7bbf62e4b11c00d3ba5d8d45594fc6c9fd6ebb90ea5ee43ef165c7	not set	2023-01-03 20:55:00.921286+00	2
Adelheid	2d3280e1b732ad4728a3109c83e0c72ca853f6409bb46a6c750e38868da4c9bba9f5b2b255f2576ff27ccdbd1b969d7ce89ae187515d553ab7dad9279112205d	not set	2023-01-03 20:55:01.010573+00	2
Adelia	e2a73fc1402070e91bf60bb1a09d92cb0522309708887a1a4732253da2ab7f1c227b02a0c3630f338d6e38f80964eb562a8bd05a444a4cf75fa96b8122e537c7	not set	2023-01-03 20:55:01.075099+00	2
\.


--
-- Data for Name: monthlyfinancialanalysis; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.monthlyfinancialanalysis (month, expenses, income, gymcode) FROM stdin;
\.


--
-- Data for Name: payment; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.payment (checknum, clientid, gymcode, paiddate, amount, expirydate, paymentinfo, status, refmb) FROM stdin;
\.


--
-- Data for Name: paymenttype; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.paymenttype (type, paymentchecknum, description) FROM stdin;
\.


--
-- Data for Name: physicalconditionbymonth; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.physicalconditionbymonth (clientid, month, height, weight, imc) FROM stdin;
\.


--
-- Data for Name: refmb; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.refmb (reference, refexpirydate) FROM stdin;
\.


--
-- Data for Name: statisticsbymonth; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.statisticsbymonth (clientid, month, runneddistance, estimatedlostcalories, besttimetorunamile, averagehearthratebeats, averageoverallperformance) FROM stdin;
\.


--
-- Data for Name: subscription; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.subscription (clientid, subscriptiontype, requiredpaymentdate, startdate, status, automaticrenewal, comments, nofcancelations) FROM stdin;
\.


--
-- Data for Name: subscriptiontype; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.subscriptiontype (type, description, normalprice) FROM stdin;
\.


--
-- Data for Name: trainer; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.trainer (id, logindatausername, gymcode, rating) FROM stdin;
\.


--
-- Data for Name: userdata; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.userdata (logindatausername, firstname, lastname, birthdate, gender, nif, phone, usersince, lastpasswreset) FROM stdin;
IpcaGymAdmin	default	admin	1999-01-01 00:00:00+00	Unspecified	10000000	923667623	2023-01-03 20:54:58.066283+00	\N
Aaren	melicia	friens	2001-06-05 00:00:00+01	Male	428022482	965788608	2023-01-03 20:54:58.307718+00	\N
Aarika	joel	jillian	2006-01-05 00:00:00+00	Female	764398764	927983600	2023-01-03 20:54:58.370351+00	\N
Aaron	jorge	tyson	1994-02-11 00:00:00+01	Female	178038533	998815118	2023-01-03 20:54:58.42876+00	\N
Aartjan	braysher	lagreula	1999-07-05 00:00:00+01	Male	547194097	924511590	2023-01-03 20:54:58.482718+00	\N
Abagael	lynn	spence	1998-11-05 00:00:00+00	Female	814985991	980044696	2023-01-03 20:54:58.532197+00	\N
Abagail	edwin	drower	2013-07-06 00:00:00+01	Male	597963092	954399824	2023-01-03 20:54:58.585533+00	\N
Abahri	kira	hyun	2006-01-11 00:00:00+00	Female	537356215	905272848	2023-01-03 20:54:58.63621+00	\N
Abbas	jonny	mechado	2009-01-01 00:00:00+00	Female	179782097	985440995	2023-01-03 20:54:58.685755+00	\N
Abbe	thorbjoern	gupta	2017-03-01 00:00:00+00	Male	236352449	990198637	2023-01-03 20:54:58.734077+00	\N
Abbey	suzy	madigan	2003-02-07 00:00:00+00	Female	735514482	973629415	2023-01-03 20:54:58.797081+00	\N
Abbi	amma	olga	2010-05-02 00:00:00+01	Female	304545930	943721885	2023-01-03 20:54:58.848044+00	\N
Abbie	gregorio	yun	1995-06-11 00:00:00+02	Female	890345788	920957932	2023-01-03 20:54:58.905578+00	\N
Abby	eliane	corradino	1990-02-05 00:00:00+00	Male	443585276	941577878	2023-01-03 20:54:58.955838+00	\N
Abbye	mohamcel	mahina	1992-10-06 00:00:00+01	Female	373777821	947723182	2023-01-03 20:54:59.061255+00	\N
Abdalla	jasbir	jozic	2001-01-08 00:00:00+00	Female	658116712	901331465	2023-01-03 20:54:59.110905+00	\N
Abdallah	aurore	ferragamo	2000-04-11 00:00:00+01	Female	276649200	977334879	2023-01-03 20:54:59.159055+00	\N
Abdul	manlio	group	1995-08-10 00:00:00+02	Female	645549769	915382876	2023-01-03 20:54:59.219375+00	\N
Abdullah	roxie	boiz	1992-04-11 00:00:00+01	Female	366152319	938579639	2023-01-03 20:54:59.271993+00	\N
Abe	brenda	arriaga	1995-03-06 00:00:00+01	Female	999100414	921237016	2023-01-03 20:54:59.323306+00	\N
Abel	santino	guiliani	1995-01-08 00:00:00+01	Male	960742130	995623345	2023-01-03 20:54:59.376212+00	\N
Abigael	shoula	jordan	2010-02-11 00:00:00+00	Male	166515964	925097139	2023-01-03 20:54:59.431542+00	\N
Abigail	carling	aranda	1991-11-11 00:00:00+00	Female	907789139	937017334	2023-01-03 20:54:59.480797+00	\N
Abigale	petra	sloan	2011-07-01 00:00:00+01	Male	572745193	929391186	2023-01-03 20:54:59.528666+00	\N
Abra	arely	alcapone	2005-07-04 00:00:00+01	Female	399494569	994261314	2023-01-03 20:54:59.58556+00	\N
Abraham	lourdes	prempeh	2002-09-08 00:00:00+01	Male	624893026	952483908	2023-01-03 20:54:59.652731+00	\N
Abu	christiansen	shepard	2005-06-04 00:00:00+01	Female	465136957	998420803	2023-01-03 20:54:59.704257+00	\N
Access	lettie	extras	2009-07-05 00:00:00+01	Female	930151384	986930853	2023-01-03 20:54:59.755996+00	\N
Accounting	callista	bruner	1990-11-09 00:00:00+00	Female	821522975	977082055	2023-01-03 20:54:59.812726+00	\N
Achal	renetao	tremblay	2009-08-03 00:00:00+01	Female	560575057	996839758	2023-01-03 20:54:59.867915+00	\N
Achamma	michael	hennessy	2000-05-05 00:00:00+01	Male	147483726	934175976	2023-01-03 20:54:59.925285+00	\N
Action	phung	zaitz	2008-06-11 00:00:00+01	Male	968903883	933379811	2023-01-03 20:54:59.977141+00	\N
Ada	sufi	dimas	2011-09-05 00:00:00+01	Female	797438979	936345569	2023-01-03 20:55:00.031521+00	\N
Adah	mina	rhodes	1998-11-11 00:00:00+00	Female	263470238	991235523	2023-01-03 20:55:00.081331+00	\N
Adaline	roberta	hanel	2000-04-02 00:00:00+01	Female	821252209	917462751	2023-01-03 20:55:00.130077+00	\N
Adam	nathallie	dieter	2010-11-01 00:00:00+00	Female	820327987	998446003	2023-01-03 20:55:00.182504+00	\N
Adan	picazo	heydt	2013-06-09 00:00:00+01	Male	414801980	991258366	2023-01-03 20:55:00.241988+00	\N
Adara	natalya	dove	2013-04-06 00:00:00+01	Male	822159964	966545740	2023-01-03 20:55:00.291303+00	\N
Adda	janell	mizarahi	2016-05-07 00:00:00+01	Female	489111332	963505137	2023-01-03 20:55:00.339968+00	\N
Addi	family	vendruscolo	2017-05-02 00:00:00+01	Female	634936038	914027010	2023-01-03 20:55:00.38849+00	\N
Addia	sholeh	yaga	1990-08-09 00:00:00+01	Male	667786625	986860395	2023-01-03 20:55:00.443904+00	\N
Addie	camilo	berkowitz	1996-01-05 00:00:00+01	Female	908806987	901187533	2023-01-03 20:55:00.492484+00	\N
Addons	elvira	pateras	1990-02-05 00:00:00+00	Female	339871387	963764684	2023-01-03 20:55:00.539944+00	\N
Addy	dey	sheedy	2009-04-10 00:00:00+01	Female	784701281	926458652	2023-01-03 20:55:00.591061+00	\N
Adel	kuehne	baal	2016-05-06 00:00:00+01	Male	479721357	952136197	2023-01-03 20:55:00.644665+00	\N
Adela	darlenes	gordon	2005-09-09 00:00:00+01	Female	901575015	986717700	2023-01-03 20:55:00.736737+00	\N
Adelaida	rebecca	wilkins	2000-10-09 00:00:00+01	Female	444216692	911394969	2023-01-03 20:55:00.831709+00	\N
Adelaide	amarilis	mcvey	2018-05-03 00:00:00+01	Male	829588189	993340446	2023-01-03 20:55:00.88311+00	\N
Adele	nansy	naccarato	1993-05-02 00:00:00+02	Male	512847565	941380134	2023-01-03 20:55:00.935064+00	\N
Adelheid	ananya	palma	2014-08-07 00:00:00+01	Female	173430526	922823815	2023-01-03 20:55:01.031939+00	\N
Adelia	jhn	antonio	1994-10-11 00:00:00+01	Male	363982328	908509849	2023-01-03 20:55:01.086676+00	\N
\.


--
-- Name: creditcard_ccnum_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.creditcard_ccnum_seq', 1, false);


--
-- Name: emailtype_type_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.emailtype_type_seq', 1, false);


--
-- Name: eventtype_type_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.eventtype_type_seq', 1, false);


--
-- Name: exteriorspacetype_type_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.exteriorspacetype_type_seq', 1, false);


--
-- Name: paymenttype_type_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.paymenttype_type_seq', 1, false);


--
-- Name: refmb_reference_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.refmb_reference_seq', 1, false);


--
-- Name: subscriptiontype_type_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.subscriptiontype_type_seq', 1, false);


--
-- Name: address address_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.address
    ADD CONSTRAINT address_pkey PRIMARY KEY (code);


--
-- Name: admin admin_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT admin_pkey PRIMARY KEY (id);


--
-- Name: client client_logindatausername_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_logindatausername_key UNIQUE (logindatausername);


--
-- Name: client client_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_pkey PRIMARY KEY (id);


--
-- Name: creditcard creditcard_clientid_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.creditcard
    ADD CONSTRAINT creditcard_clientid_key UNIQUE (clientid);


--
-- Name: creditcard creditcard_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.creditcard
    ADD CONSTRAINT creditcard_pkey PRIMARY KEY (ccnum);


--
-- Name: devicelogin devicelogin_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.devicelogin
    ADD CONSTRAINT devicelogin_pkey PRIMARY KEY (globalip, logeddate);


--
-- Name: emailinfo emailinfo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.emailinfo
    ADD CONSTRAINT emailinfo_pkey PRIMARY KEY (email);


--
-- Name: emailtype emailtype_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.emailtype
    ADD CONSTRAINT emailtype_pkey PRIMARY KEY (type);


--
-- Name: event event_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event
    ADD CONSTRAINT event_pkey PRIMARY KEY (code);


--
-- Name: eventtype eventtype_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.eventtype
    ADD CONSTRAINT eventtype_pkey PRIMARY KEY (type);


--
-- Name: exteriorspace exteriorspace_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exteriorspace
    ADD CONSTRAINT exteriorspace_pkey PRIMARY KEY (spaceid);


--
-- Name: exteriorspacetype exteriorspacetype_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exteriorspacetype
    ADD CONSTRAINT exteriorspacetype_pkey PRIMARY KEY (type);


--
-- Name: gym gym_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.gym
    ADD CONSTRAINT gym_pkey PRIMARY KEY (code);


--
-- Name: invoice invoice_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoice
    ADD CONSTRAINT invoice_pkey PRIMARY KEY (paymentchecknum);


--
-- Name: logindata logindata_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.logindata
    ADD CONSTRAINT logindata_pkey PRIMARY KEY (username);


--
-- Name: monthlyfinancialanalysis monthlyfinancialanalysis_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.monthlyfinancialanalysis
    ADD CONSTRAINT monthlyfinancialanalysis_pkey PRIMARY KEY (month);


--
-- Name: payment payment_clientid_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment
    ADD CONSTRAINT payment_clientid_key UNIQUE (clientid);


--
-- Name: payment payment_gymcode_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment
    ADD CONSTRAINT payment_gymcode_key UNIQUE (gymcode);


--
-- Name: payment payment_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment
    ADD CONSTRAINT payment_pkey PRIMARY KEY (checknum);


--
-- Name: paymenttype paymenttype_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paymenttype
    ADD CONSTRAINT paymenttype_pkey PRIMARY KEY (type);


--
-- Name: physicalconditionbymonth physicalconditionbymonth_clientid_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicalconditionbymonth
    ADD CONSTRAINT physicalconditionbymonth_clientid_key UNIQUE (clientid);


--
-- Name: physicalconditionbymonth physicalconditionbymonth_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicalconditionbymonth
    ADD CONSTRAINT physicalconditionbymonth_pkey PRIMARY KEY (clientid, month);


--
-- Name: refmb refmb_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refmb
    ADD CONSTRAINT refmb_pkey PRIMARY KEY (reference);


--
-- Name: statisticsbymonth statisticsbymonth_clientid_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.statisticsbymonth
    ADD CONSTRAINT statisticsbymonth_clientid_key UNIQUE (clientid);


--
-- Name: statisticsbymonth statisticsbymonth_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.statisticsbymonth
    ADD CONSTRAINT statisticsbymonth_pkey PRIMARY KEY (clientid, month);


--
-- Name: subscription subscription_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.subscription
    ADD CONSTRAINT subscription_pkey PRIMARY KEY (clientid);


--
-- Name: subscriptiontype subscriptiontype_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.subscriptiontype
    ADD CONSTRAINT subscriptiontype_pkey PRIMARY KEY (type);


--
-- Name: trainer trainer_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trainer
    ADD CONSTRAINT trainer_pkey PRIMARY KEY (id);


--
-- Name: userdata userdata_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.userdata
    ADD CONSTRAINT userdata_pkey PRIMARY KEY (logindatausername);


--
-- Name: address fkaddress393741; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.address
    ADD CONSTRAINT fkaddress393741 FOREIGN KEY (username) REFERENCES public.userdata(logindatausername);


--
-- Name: admin fkadmin526491; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT fkadmin526491 FOREIGN KEY (gymcode) REFERENCES public.gym(code);


--
-- Name: admin fkadmin71591; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT fkadmin71591 FOREIGN KEY (logindatausername) REFERENCES public.logindata(username);


--
-- Name: client fkclient518585; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT fkclient518585 FOREIGN KEY (gymcode) REFERENCES public.gym(code);


--
-- Name: client fkclient892093; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT fkclient892093 FOREIGN KEY (logindatausername) REFERENCES public.logindata(username);


--
-- Name: creditcard fkcreditcard849876; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.creditcard
    ADD CONSTRAINT fkcreditcard849876 FOREIGN KEY (clientid) REFERENCES public.client(id);


--
-- Name: devicelogin fkdevicelogi241719; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.devicelogin
    ADD CONSTRAINT fkdevicelogi241719 FOREIGN KEY (logindatausername) REFERENCES public.logindata(username);


--
-- Name: emailinfo fkemailinfo392873; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.emailinfo
    ADD CONSTRAINT fkemailinfo392873 FOREIGN KEY (emailtypetype) REFERENCES public.emailtype(type);


--
-- Name: emailinfo fkemailinfo926875; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.emailinfo
    ADD CONSTRAINT fkemailinfo926875 FOREIGN KEY (logindatausername) REFERENCES public.logindata(username);


--
-- Name: event fkevent208542; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event
    ADD CONSTRAINT fkevent208542 FOREIGN KEY (eventtype) REFERENCES public.eventtype(type);


--
-- Name: event fkevent303692; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event
    ADD CONSTRAINT fkevent303692 FOREIGN KEY (gymcode) REFERENCES public.gym(code);


--
-- Name: event fkevent344055; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event
    ADD CONSTRAINT fkevent344055 FOREIGN KEY (trainerid) REFERENCES public.trainer(id);


--
-- Name: event fkevent452811; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event
    ADD CONSTRAINT fkevent452811 FOREIGN KEY (clientid) REFERENCES public.client(id);


--
-- Name: event fkevent667295; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event
    ADD CONSTRAINT fkevent667295 FOREIGN KEY (exteriorspaceid) REFERENCES public.exteriorspace(spaceid);


--
-- Name: exteriorspace fkexteriorsp430996; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exteriorspace
    ADD CONSTRAINT fkexteriorsp430996 FOREIGN KEY (exteriortype) REFERENCES public.exteriorspacetype(type);


--
-- Name: exteriorspace fkexteriorsp929037; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exteriorspace
    ADD CONSTRAINT fkexteriorsp929037 FOREIGN KEY (gymcode) REFERENCES public.gym(code);


--
-- Name: gym fkgym221766; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.gym
    ADD CONSTRAINT fkgym221766 FOREIGN KEY (addresscode) REFERENCES public.address(code);


--
-- Name: invoice fkinvoice65249; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoice
    ADD CONSTRAINT fkinvoice65249 FOREIGN KEY (paymentchecknum) REFERENCES public.payment(checknum);


--
-- Name: monthlyfinancialanalysis fkmonthlyfin331142; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.monthlyfinancialanalysis
    ADD CONSTRAINT fkmonthlyfin331142 FOREIGN KEY (gymcode) REFERENCES public.gym(code);


--
-- Name: payment fkpayment492783; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment
    ADD CONSTRAINT fkpayment492783 FOREIGN KEY (refmb) REFERENCES public.refmb(reference);


--
-- Name: payment fkpayment669813; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment
    ADD CONSTRAINT fkpayment669813 FOREIGN KEY (gymcode) REFERENCES public.gym(code);


--
-- Name: payment fkpayment818932; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payment
    ADD CONSTRAINT fkpayment818932 FOREIGN KEY (clientid) REFERENCES public.client(id);


--
-- Name: paymenttype fkpaymenttyp295765; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paymenttype
    ADD CONSTRAINT fkpaymenttyp295765 FOREIGN KEY (paymentchecknum) REFERENCES public.payment(checknum);


--
-- Name: physicalconditionbymonth fkphysicalco377299; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicalconditionbymonth
    ADD CONSTRAINT fkphysicalco377299 FOREIGN KEY (clientid) REFERENCES public.client(id);


--
-- Name: statisticsbymonth fkstatistics473745; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.statisticsbymonth
    ADD CONSTRAINT fkstatistics473745 FOREIGN KEY (clientid) REFERENCES public.client(id);


--
-- Name: subscription fksubscripti267730; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.subscription
    ADD CONSTRAINT fksubscripti267730 FOREIGN KEY (clientid) REFERENCES public.client(id);


--
-- Name: subscription fksubscripti49420; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.subscription
    ADD CONSTRAINT fksubscripti49420 FOREIGN KEY (subscriptiontype) REFERENCES public.subscriptiontype(type);


--
-- Name: trainer fktrainer202399; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trainer
    ADD CONSTRAINT fktrainer202399 FOREIGN KEY (gymcode) REFERENCES public.gym(code);


--
-- Name: trainer fktrainer395683; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trainer
    ADD CONSTRAINT fktrainer395683 FOREIGN KEY (logindatausername) REFERENCES public.logindata(username);


--
-- Name: userdata fkuserdata634915; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.userdata
    ADD CONSTRAINT fkuserdata634915 FOREIGN KEY (logindatausername) REFERENCES public.logindata(username);


--
-- PostgreSQL database dump complete
--


ALTER TABLE Payment DROP CONSTRAINT FKPayment818932;
ALTER TABLE Subscription DROP CONSTRAINT FKSubscripti267730;
ALTER TABLE Trainer DROP CONSTRAINT FKTrainer202399;
ALTER TABLE Gym DROP CONSTRAINT FKGym221766;
ALTER TABLE Payment DROP CONSTRAINT FKPayment669813;
ALTER TABLE Client DROP CONSTRAINT FKClient892093;
ALTER TABLE Trainer DROP CONSTRAINT FKTrainer395683;
ALTER TABLE Client DROP CONSTRAINT FKClient518585;
ALTER TABLE Admin DROP CONSTRAINT FKAdmin71591;
ALTER TABLE Admin DROP CONSTRAINT FKAdmin526491;
ALTER TABLE Event DROP CONSTRAINT FKEvent452811;
ALTER TABLE Event DROP CONSTRAINT FKEvent344055;
ALTER TABLE Event DROP CONSTRAINT FKEvent208542;
ALTER TABLE CreditCard DROP CONSTRAINT FKCreditCard849876;
ALTER TABLE emailInfo DROP CONSTRAINT FKemailInfo926875;
ALTER TABLE DeviceLogin DROP CONSTRAINT FKDeviceLogi241719;
ALTER TABLE ExteriorSpace DROP CONSTRAINT FKExteriorSp430996;
ALTER TABLE ExteriorSpace DROP CONSTRAINT FKExteriorSp929037;
ALTER TABLE UserData DROP CONSTRAINT FKUserData634915;
ALTER TABLE Address DROP CONSTRAINT FKAddress393741;
ALTER TABLE Event DROP CONSTRAINT FKEvent303692;
ALTER TABLE Event DROP CONSTRAINT FKEvent667295;
ALTER TABLE Payment DROP CONSTRAINT FKPayment492783;
ALTER TABLE PaymentType DROP CONSTRAINT FKPaymentTyp295765;
ALTER TABLE Invoice DROP CONSTRAINT FKInvoice65249;
ALTER TABLE MonthlyFinancialAnalysis DROP CONSTRAINT FKMonthlyFin331142;
ALTER TABLE PhysicalConditionByMonth DROP CONSTRAINT FKPhysicalCo377299;
ALTER TABLE Subscription DROP CONSTRAINT FKSubscripti49420;
ALTER TABLE emailInfo DROP CONSTRAINT FKemailInfo392873;
ALTER TABLE StatisticsByMonth DROP CONSTRAINT FKStatistics473745;
DROP TABLE IF EXISTS Address CASCADE;
DROP TABLE IF EXISTS Admin CASCADE;
DROP TABLE IF EXISTS Client CASCADE;
DROP TABLE IF EXISTS CreditCard CASCADE;
DROP TABLE IF EXISTS DeviceLogin CASCADE;
DROP TABLE IF EXISTS emailInfo CASCADE;
DROP TABLE IF EXISTS EmailType CASCADE;
DROP TABLE IF EXISTS Event CASCADE;
DROP TABLE IF EXISTS EventType CASCADE;
DROP TABLE IF EXISTS ExteriorSpace CASCADE;
DROP TABLE IF EXISTS ExteriorSpaceType CASCADE;
DROP TABLE IF EXISTS Gym CASCADE;
DROP TABLE IF EXISTS Invoice CASCADE;
DROP TABLE IF EXISTS LoginData CASCADE;
DROP TABLE IF EXISTS MonthlyFinancialAnalysis CASCADE;
DROP TABLE IF EXISTS Payment CASCADE;
DROP TABLE IF EXISTS PaymentType CASCADE;
DROP TABLE IF EXISTS PhysicalConditionByMonth CASCADE;
DROP TABLE IF EXISTS RefMb CASCADE;
DROP TABLE IF EXISTS StatisticsByMonth CASCADE;
DROP TABLE IF EXISTS Subscription CASCADE;
DROP TABLE IF EXISTS SubscriptionType CASCADE;
DROP TABLE IF EXISTS Trainer CASCADE;
DROP TABLE IF EXISTS UserData CASCADE;
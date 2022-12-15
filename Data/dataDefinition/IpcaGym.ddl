CREATE TABLE IF NOT EXISTS SubscriptionType (
  type        SERIAL NOT NULL, 
  Description varchar(255) NOT NULL, 
  NormalPrice float8 NOT NULL, 
  PRIMARY KEY (type));

CREATE TABLE IF NOT EXISTS Subscription (
  ClientID            uuid NOT NULL, 
  SubscriptionType    int4 NOT NULL, 
  RequiredPaymentDate date, 
  StartDate           date, 
  Status              int4, 
  AutomaticRenewal    bool, 
  Comments            varchar(255), 
  NOfCancelations     int4, 
  PRIMARY KEY (ClientID));

CREATE TABLE IF NOT EXISTS Payment (
  CheckNum    uuid NOT NULL, 
  ClientID    uuid NOT NULL UNIQUE, 
  GymCode     uuid NOT NULL UNIQUE, 
  PaidDate    date, 
  Amount      float8 NOT NULL, 
  ExpiryDate  date NOT NULL, 
  PaymentInfo varchar(255) NOT NULL, 
  status      varchar(255) NOT NULL, 
  RefMb       int4, 
  PRIMARY KEY (CheckNum));

CREATE TABLE IF NOT EXISTS Client (
  ID                uuid NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL UNIQUE, 
  GymCode           uuid NOT NULL, 
  PRIMARY KEY (ID));

CREATE TABLE IF NOT EXISTS Trainer (
  ID                uuid NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL, 
  GymCode           uuid NOT NULL, 
  Rating            float8, 
  PRIMARY KEY (ID));

CREATE TABLE IF NOT EXISTS Gym (
  Code              uuid NOT NULL, 
  ClientCapacity    int4 NOT NULL, 
  CurrentNofClients int4 NOT NULL, 
  Nif               int4, 
  MbEntiity         varchar(255), 
  AddressCode       uuid NOT NULL, 
  PRIMARY KEY (Code));

CREATE TABLE IF NOT EXISTS Address (
  Code           uuid NOT NULL, 
  PostalCode     int4 NOT NULL, 
  Country        varchar(255) NOT NULL, 
  City           varchar(255) NOT NULL, 
  LastUpdateDate date, 
  AditionalInfo  varchar(255), 
  HouseNum       int4 NOT NULL, 
  Localidade     int4 NOT NULL, 
  Username       varchar(255) NOT NULL, 
  PRIMARY KEY (Code));

CREATE TABLE IF NOT EXISTS LoginData (
  Username         varchar(255) NOT NULL, 
  HashedPassword   varchar(255) NOT NULL, 
  TwoFactorAuthApp varchar(255), 
  LastLogin        date, 
  PRIMARY KEY (Username));

CREATE TABLE IF NOT EXISTS emailInfo (
  email             varchar(255) NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL, 
  Validated         int4 NOT NULL, 
  EmailTypeType     int4 NOT NULL, 
  SubscrivedToNews  bool, 
  PRIMARY KEY (email));

CREATE TABLE IF NOT EXISTS Device (
  GlobalIp          varchar(255) NOT NULL, 
  LogedDate         date NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL, 
  Browser           varchar(255) NOT NULL, 
  OS                varchar(255) NOT NULL, 
  IPLocationLat     float4 NOT NULL, 
  IPLocationLng     float4 NOT NULL, 
  PRIMARY KEY (GlobalIp, 
  LogedDate));

CREATE TABLE IF NOT EXISTS CreditCard (
  CCnum        SERIAL NOT NULL, 
  ClientID     uuid NOT NULL UNIQUE, 
  ExpiryDate   date NOT NULL, 
  InsertedDate date NOT NULL, 
  SecurityCode int4 NOT NULL, 
  NameInCC     int4 NOT NULL, 
  PRIMARY KEY (CCnum));

CREATE TABLE IF NOT EXISTS Event (
  Code                 uuid NOT NULL, 
  ClientID             uuid NOT NULL, 
  TrainerID            uuid, 
  EventType            int4 NOT NULL, 
  ExteriorSpaceSpaceID uuid, 
  GymCode              uuid NOT NULL, 
  StartDate            date NOT NULL, 
  EndDate              date NOT NULL, 
  Comments             varchar(255), 
  PRIMARY KEY (Code));

CREATE TABLE IF NOT EXISTS Admin (
  ID                uuid NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL, 
  GymCode           uuid NOT NULL, 
  PRIMARY KEY (ID));

CREATE TABLE IF NOT EXISTS TrainersScheduleByMonth (
  TrainerID uuid NOT NULL, 
  Month     date NOT NULL, 
  daysOff   varchar(255) NOT NULL, 
  PRIMARY KEY (TrainerID, 
  Month));

CREATE TABLE IF NOT EXISTS EventType (
  Type        SERIAL NOT NULL, 
  Description varchar(255) NOT NULL, 
  PRIMARY KEY (Type));

CREATE TABLE IF NOT EXISTS EncryptedArea (
  Email  varchar(255) NOT NULL, 
  field1 varchar(255), 
  field2 varchar(255), 
  field3 varchar(255), 
  PRIMARY KEY (Email));

CREATE TABLE IF NOT EXISTS Payment (
  CheckNum    varchar(255) NOT NULL, 
  CustomerID  int4 NOT NULL, 
  PaymentDate date, 
  Amount      numeric(19, 0), 
  PRIMARY KEY (CheckNum));

CREATE TABLE IF NOT EXISTS ExteriorSpace (
  SpaceID           uuid NOT NULL, 
  ClientCapacity    int4 NOT NULL, 
  CurrentNofClients int4 NOT NULL, 
  Area              float8, 
  ExteriorType      int4 NOT NULL, 
  GymCode           uuid NOT NULL, 
  PRIMARY KEY (SpaceID));

CREATE TABLE IF NOT EXISTS ExteriorSpaceType (
  Type        SERIAL NOT NULL, 
  Description varchar(255), 
  PRIMARY KEY (Type));

CREATE TABLE IF NOT EXISTS UserData (
  LoginDataUsername  varchar(255) NOT NULL, 
  FirstName          int4 NOT NULL, 
  LastName           int4 NOT NULL, 
  BirthDate          date NOT NULL, 
  Gender             int4 NOT NULL, 
  Nif                int4 NOT NULL, 
  phone              int4 NOT NULL, 
  UserSince          date NOT NULL, 
  LastPasswReset     date, 
  TimeZoneIdentifier varchar(255), 
  PRIMARY KEY (LoginDataUsername));

CREATE TABLE IF NOT EXISTS Objective (
  ClientID    uuid NOT NULL, 
  Description varchar(255) NOT NULL, 
  PRIMARY KEY (ClientID));

CREATE TABLE IF NOT EXISTS PaymentType (
  Type            SERIAL NOT NULL, 
  PaymentCheckNum uuid NOT NULL, 
  Description     varchar(255) NOT NULL, 
  PRIMARY KEY (Type));

CREATE TABLE IF NOT EXISTS RefMb (
  reference     SERIAL NOT NULL, 
  RefExpiryDate int4 NOT NULL, 
  PRIMARY KEY (reference));

CREATE TABLE IF NOT EXISTS Invoice (
  PaymentCheckNum uuid NOT NULL, 
  Tax             int4, 
  Email           bool, 
  IncludeNif      bool, 
  PRIMARY KEY (PaymentCheckNum));

CREATE TABLE IF NOT EXISTS MonthlyFinancialAnalysis (
  month    date NOT NULL, 
  Expenses float8 NOT NULL, 
  Income   float8 NOT NULL, 
  GymCode  uuid NOT NULL, 
  PRIMARY KEY (month));

CREATE TABLE IF NOT EXISTS StatisticsByMonth (
  ClientID                  uuid NOT NULL UNIQUE, 
  Month                     date NOT NULL, 
  RunnedDistance            float8, 
  EstimatedLostCalories     float8, 
  BestTimeToRunAMile        float8, 
  AverageHearthRateBeats    float8, 
  AverageOverallPerformance float8, 
  PRIMARY KEY (ClientID, 
  Month));

CREATE TABLE IF NOT EXISTS PhysicalConditionByMonth (
  ClientID uuid NOT NULL UNIQUE, 
  month    date NOT NULL, 
  Height   float8, 
  Weight   float8, 
  IMC      float8, 
  PRIMARY KEY (ClientID, 
  month));

CREATE TABLE IF NOT EXISTS EmailType (
  Type        SERIAL NOT NULL, 
  Description varchar(255) NOT NULL, 
  PRIMARY KEY (Type));

ALTER TABLE Payment ADD CONSTRAINT FKPayment818932 FOREIGN KEY (ClientID) REFERENCES Client (ID);
ALTER TABLE Subscription ADD CONSTRAINT FKSubscripti267730 FOREIGN KEY (ClientID) REFERENCES Client (ID);
ALTER TABLE Trainer ADD CONSTRAINT FKTrainer202399 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE Gym ADD CONSTRAINT FKGym221766 FOREIGN KEY (AddressCode) REFERENCES Address (Code);
ALTER TABLE Payment ADD CONSTRAINT FKPayment669813 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE Client ADD CONSTRAINT FKClient892093 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE Trainer ADD CONSTRAINT FKTrainer395683 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE Client ADD CONSTRAINT FKClient518585 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE Admin ADD CONSTRAINT FKAdmin71591 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE Admin ADD CONSTRAINT FKAdmin526491 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE Event ADD CONSTRAINT FKEvent452811 FOREIGN KEY (ClientID) REFERENCES Client (ID);
ALTER TABLE Event ADD CONSTRAINT FKEvent344055 FOREIGN KEY (TrainerID) REFERENCES Trainer (ID);
ALTER TABLE Event ADD CONSTRAINT FKEvent208542 FOREIGN KEY (EventType) REFERENCES EventType (Type);
ALTER TABLE CreditCard ADD CONSTRAINT FKCreditCard849876 FOREIGN KEY (ClientID) REFERENCES Client (ID);
ALTER TABLE Payment ADD CONSTRAINT FKPayment75777 FOREIGN KEY (CustomerID) REFERENCES Customer (ID);
ALTER TABLE "Order" ADD CONSTRAINT FKOrder556711 FOREIGN KEY (CustomerID) REFERENCES Customer (ID);
ALTER TABLE Order_Product ADD CONSTRAINT FKOrder_Prod922282 FOREIGN KEY (OrderID) REFERENCES "Order" (ID);
ALTER TABLE Order_Product ADD CONSTRAINT FKOrder_Prod898397 FOREIGN KEY (ProductCode) REFERENCES Product (Code);
ALTER TABLE Product ADD CONSTRAINT FKProduct65612 FOREIGN KEY (ProductlineID) REFERENCES Productline (ID);
ALTER TABLE Employee ADD CONSTRAINT FKEmployee139413 FOREIGN KEY (OfficeCode) REFERENCES Office (Code);
ALTER TABLE Employee ADD CONSTRAINT FKEmployee50179 FOREIGN KEY (reportsTo) REFERENCES Employee (ID);
ALTER TABLE Customer ADD CONSTRAINT FKCustomer464773 FOREIGN KEY (salesRepEmployeeNum) REFERENCES Employee (ID);
ALTER TABLE emailInfo ADD CONSTRAINT FKemailInfo926875 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE Device ADD CONSTRAINT FKDevice336796 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE ExteriorSpace ADD CONSTRAINT FKExteriorSp430996 FOREIGN KEY (ExteriorType) REFERENCES ExteriorSpaceType (Type);
ALTER TABLE ExteriorSpace ADD CONSTRAINT FKExteriorSp929037 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE UserData ADD CONSTRAINT FKUserData634915 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE Address ADD CONSTRAINT FKAddress393741 FOREIGN KEY (Username) REFERENCES UserData (LoginDataUsername);
ALTER TABLE Event ADD CONSTRAINT FKEvent303692 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE Event ADD CONSTRAINT FKEvent485953 FOREIGN KEY (ExteriorSpaceSpaceID) REFERENCES ExteriorSpace (SpaceID);
ALTER TABLE TrainersScheduleByMonth ADD CONSTRAINT FKTrainersSc974661 FOREIGN KEY (TrainerID) REFERENCES Trainer (ID);
ALTER TABLE Payment ADD CONSTRAINT FKPayment492783 FOREIGN KEY (RefMb) REFERENCES RefMb (reference);
ALTER TABLE PaymentType ADD CONSTRAINT FKPaymentTyp295765 FOREIGN KEY (PaymentCheckNum) REFERENCES Payment (CheckNum);
ALTER TABLE Invoice ADD CONSTRAINT FKInvoice65249 FOREIGN KEY (PaymentCheckNum) REFERENCES Payment (CheckNum);
ALTER TABLE MonthlyFinancialAnalysis ADD CONSTRAINT FKMonthlyFin331142 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE PhysicalConditionByMonth ADD CONSTRAINT FKPhysicalCo377299 FOREIGN KEY (ClientID) REFERENCES Client (ID);
ALTER TABLE Objective ADD CONSTRAINT FKObjective409133 FOREIGN KEY (ClientID) REFERENCES Client (ID);
ALTER TABLE Subscription ADD CONSTRAINT FKSubscripti49420 FOREIGN KEY (SubscriptionType) REFERENCES SubscriptionType (type);
ALTER TABLE emailInfo ADD CONSTRAINT FKemailInfo392873 FOREIGN KEY (EmailTypeType) REFERENCES EmailType (Type);
ALTER TABLE StatisticsByMonth ADD CONSTRAINT FKStatistics473745 FOREIGN KEY (ClientID) REFERENCES Client (ID);

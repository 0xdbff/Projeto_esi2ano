CREATE TABLE Address (
  Code           uuid NOT NULL, 
  PostalCode     varchar(255) NOT NULL, 
  Country        varchar(255) NOT NULL, 
  City           varchar(255) NOT NULL, 
  LastUpdateDate timestamp with time zone, 
  AditionalInfo  varchar(255), 
  HouseNum       int4 NOT NULL, 
  Localidade     varchar(255) NOT NULL, 
  Username       varchar(255) NOT NULL, 
  PRIMARY KEY (Code));

CREATE TABLE Admin (
  ID                uuid NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL, 
  GymCode           uuid NOT NULL, 
  PRIMARY KEY (ID));

CREATE TABLE Client (
  ID                uuid NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL UNIQUE, 
  GymCode           uuid NOT NULL, 
  PRIMARY KEY (ID));

CREATE TABLE CreditCard (
  CCnum        SERIAL NOT NULL, 
  ClientID     uuid NOT NULL UNIQUE, 
  ExpiryDate   timestamp with time zone NOT NULL, 
  InsertedDate timestamp with time zone NOT NULL, 
  SecurityCode int4 NOT NULL, 
  NameInCC     varchar(255) NOT NULL, 
  PRIMARY KEY (CCnum));

CREATE TABLE DeviceLogin (
  GlobalIp          varchar(255) NOT NULL, 
  LogedDate         timestamp with time zone NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL, 
  Browser           varchar(255) NOT NULL, 
  OS                varchar(255) NOT NULL, 
  IPLocationLat     float4 NOT NULL, 
  IPLocationLng     float4 NOT NULL, 
  PRIMARY KEY (GlobalIp, 
  LogedDate));

CREATE TABLE emailInfo (
  email             varchar(255) NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL, 
  Validated         int4 NOT NULL, 
  EmailTypeType     int4 NOT NULL, 
  SubscrivedToNews  bool, 
  PRIMARY KEY (email));

CREATE TABLE EmailType (
  Type        SERIAL NOT NULL, 
  Description varchar(255) NOT NULL, 
  PRIMARY KEY (Type));

CREATE TABLE Event (
  Code            uuid NOT NULL, 
  ClientID        uuid NOT NULL, 
  TrainerID       uuid, 
  EventType       int4 NOT NULL, 
  ExteriorSpaceID uuid, 
  GymCode         uuid NOT NULL, 
  StartDate       timestamp with time zone NOT NULL, 
  EndDate         timestamp with time zone NOT NULL, 
  Comments        varchar(255), 
  PRIMARY KEY (Code));

CREATE TABLE EventType (
  Type        SERIAL NOT NULL, 
  Description varchar(255) NOT NULL, 
  PRIMARY KEY (Type));

CREATE TABLE ExteriorSpace (
  SpaceID           uuid NOT NULL, 
  ClientCapacity    int4 NOT NULL, 
  CurrentNofClients int4 NOT NULL, 
  Area              float8 NOT NULL, 
  ExteriorType      int4 NOT NULL, 
  GymCode           uuid NOT NULL, 
  PRIMARY KEY (SpaceID));

CREATE TABLE ExteriorSpaceType (
  Type        SERIAL NOT NULL, 
  Description varchar(255), 
  PRIMARY KEY (Type));

CREATE TABLE Gym (
  Code              uuid NOT NULL, 
  ClientCapacity    int4 NOT NULL, 
  CurrentNofClients int4 NOT NULL, 
  Nif               int4, 
  MbEntiity         varchar(255), 
  AddressCode       uuid NOT NULL, 
  PRIMARY KEY (Code));

CREATE TABLE Invoice (
  PaymentCheckNum uuid NOT NULL, 
  Tax             int4, 
  Email           bool, 
  IncludeNif      bool, 
  PRIMARY KEY (PaymentCheckNum));

CREATE TABLE LoginData (
  Username         varchar(255) NOT NULL, 
  HashedPassword   varchar(255) NOT NULL, 
  TwoFactorAuthApp varchar(255), 
  LastLogin        timestamp with time zone, 
  usertype         int4 NOT NULL,
  PRIMARY KEY (Username));

CREATE TABLE MonthlyFinancialAnalysis (
  month    date NOT NULL, 
  Expenses float8 NOT NULL, 
  Income   float8 NOT NULL, 
  GymCode  uuid NOT NULL, 
  PRIMARY KEY (month));

CREATE TABLE Payment (
  CheckNum    uuid NOT NULL, 
  ClientID    uuid NOT NULL UNIQUE, 
  GymCode     uuid NOT NULL UNIQUE, 
  PaidDate    timestamp with time zone, 
  Amount      float8 NOT NULL, 
  ExpiryDate  timestamp with time zone NOT NULL, 
  PaymentInfo varchar(255) NOT NULL, 
  status      varchar(255) NOT NULL, 
  RefMb       int4, 
  PRIMARY KEY (CheckNum));

CREATE TABLE PaymentType (
  Type            SERIAL NOT NULL, 
  PaymentCheckNum uuid NOT NULL, 
  Description     varchar(255) NOT NULL, 
  PRIMARY KEY (Type));

CREATE TABLE PhysicalConditionByMonth (
  ClientID uuid NOT NULL UNIQUE, 
  month    date NOT NULL, 
  Height   float8, 
  Weight   float8, 
  IMC      float8, 
  PRIMARY KEY (ClientID, 
  month));

CREATE TABLE RefMb (
  reference     SERIAL NOT NULL, 
  RefExpiryDate int4 NOT NULL, 
  PRIMARY KEY (reference));

CREATE TABLE StatisticsByMonth (
  ClientID                  uuid NOT NULL UNIQUE, 
  Month                     date NOT NULL, 
  RunnedDistance            float8, 
  EstimatedLostCalories     float8, 
  BestTimeToRunAMile        float8, 
  AverageHearthRateBeats    float8, 
  AverageOverallPerformance float8, 
  PRIMARY KEY (ClientID, 
  Month));

CREATE TABLE Subscription (
  ClientID            uuid NOT NULL, 
  SubscriptionType    int4 NOT NULL, 
  RequiredPaymentDate timestamp with time zone NOT NULL, 
  StartDate           timestamp with time zone NOT NULL, 
  Status              int4 NOT NULL, 
  AutomaticRenewal    bool NOT NULL, 
  Comments            varchar(255), 
  NOfCancelations     int4 NOT NULL, 
  PRIMARY KEY (ClientID));

CREATE TABLE SubscriptionType (
  type        SERIAL NOT NULL, 
  Description varchar(255) NOT NULL, 
  NormalPrice float8 NOT NULL, 
  PRIMARY KEY (type));

CREATE TABLE Trainer (
  ID                uuid NOT NULL, 
  LoginDataUsername varchar(255) NOT NULL, 
  GymCode           uuid NOT NULL, 
  Rating            float8, 
  PRIMARY KEY (ID));

CREATE TABLE UserData (
  LoginDataUsername varchar(255) NOT NULL, 
  FirstName         varchar(255) NOT NULL, 
  LastName          varchar(255) NOT NULL, 
  BirthDate         timestamp with time zone NOT NULL, 
  Gender            int4 NOT NULL, 
  Nif               int4 NOT NULL, 
  phone             int4 NOT NULL, 
  UserSince         timestamp with time zone NOT NULL, 
  LastPasswReset    timestamp with time zone, 
  PRIMARY KEY (LoginDataUsername));

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
ALTER TABLE emailInfo ADD CONSTRAINT FKemailInfo926875 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE DeviceLogin ADD CONSTRAINT FKDeviceLogi241719 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE ExteriorSpace ADD CONSTRAINT FKExteriorSp430996 FOREIGN KEY (ExteriorType) REFERENCES ExteriorSpaceType (Type);
ALTER TABLE ExteriorSpace ADD CONSTRAINT FKExteriorSp929037 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE UserData ADD CONSTRAINT FKUserData634915 FOREIGN KEY (LoginDataUsername) REFERENCES LoginData (Username);
ALTER TABLE Address ADD CONSTRAINT FKAddress393741 FOREIGN KEY (Username) REFERENCES UserData (LoginDataUsername);
ALTER TABLE Event ADD CONSTRAINT FKEvent303692 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE Event ADD CONSTRAINT FKEvent667295 FOREIGN KEY (ExteriorSpaceID) REFERENCES ExteriorSpace (SpaceID);
ALTER TABLE Payment ADD CONSTRAINT FKPayment492783 FOREIGN KEY (RefMb) REFERENCES RefMb (reference);
ALTER TABLE PaymentType ADD CONSTRAINT FKPaymentTyp295765 FOREIGN KEY (PaymentCheckNum) REFERENCES Payment (CheckNum);
ALTER TABLE Invoice ADD CONSTRAINT FKInvoice65249 FOREIGN KEY (PaymentCheckNum) REFERENCES Payment (CheckNum);
ALTER TABLE MonthlyFinancialAnalysis ADD CONSTRAINT FKMonthlyFin331142 FOREIGN KEY (GymCode) REFERENCES Gym (Code);
ALTER TABLE PhysicalConditionByMonth ADD CONSTRAINT FKPhysicalCo377299 FOREIGN KEY (ClientID) REFERENCES Client (ID);
ALTER TABLE Subscription ADD CONSTRAINT FKSubscripti49420 FOREIGN KEY (SubscriptionType) REFERENCES SubscriptionType (type);
ALTER TABLE emailInfo ADD CONSTRAINT FKemailInfo392873 FOREIGN KEY (EmailTypeType) REFERENCES EmailType (Type);
ALTER TABLE StatisticsByMonth ADD CONSTRAINT FKStatistics473745 FOREIGN KEY (ClientID) REFERENCES Client (ID);

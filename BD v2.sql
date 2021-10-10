CREATE TABLE Client (
  Id        int IDENTITY NOT NULL, 
  Nom       varchar(255) NOT NULL, 
  Prenom    varchar(255) NOT NULL, 
  Adresse   varchar(255) NOT NULL, 
  Email     varchar(255) NOT NULL, 
  Telephone int NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Etat (
  Id          int IDENTITY(1, 1) NOT NULL, 
  Nom         varchar(255) NOT NULL UNIQUE, 
  Description varchar(255) NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Facture (
  Id           int IDENTITY(1, 1) NOT NULL, 
  Reference    varchar(255) NOT NULL, 
  DateCreation date NOT NULL, 
  Remise       int NOT NULL, 
  Tva          float(10) NOT NULL, 
  MontantHt    float(10) NOT NULL, 
  MontantTtc   float(10) NOT NULL, 
  IdStructure  int NOT NULL, 
  IdClient     int NOT NULL, 
  IdEtat       int NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Prestation (
  Id           int IDENTITY(1, 1) NOT NULL, 
  Intitule     varchar(255) NOT NULL, 
  Description  varchar(255) NOT NULL, 
  PrixUnitaire int NOT NULL, 
  Quantite     int NOT NULL, 
  PrixTotal    int NOT NULL, 
  IdFacture    int NOT NULL, 
  PRIMARY KEY (Id));
ALTER TABLE Facture ADD CONSTRAINT FKFacture569705 FOREIGN KEY (IdEtat) REFERENCES Etat (Id);
ALTER TABLE Facture ADD CONSTRAINT FKFacture214707 FOREIGN KEY (IdClient) REFERENCES Client (Id);
ALTER TABLE Prestation ADD CONSTRAINT FKPrestation198894 FOREIGN KEY (IdFacture) REFERENCES Facture (Id);
ALTER TABLE Facture ADD CONSTRAINT FKFacture840039 FOREIGN KEY (IdStructure) REFERENCES Structure (Id);

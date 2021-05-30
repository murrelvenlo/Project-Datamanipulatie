
ALTER TABLE	Vluchtboeking.Reservering DROP CONSTRAINT FK_Reservering_Passagier;
ALTER TABLE	Vluchtboeking.Reservering DROP CONSTRAINT FK_Reservering_Klasse;
ALTER TABLE Vluchtboeking.Reserveringvlucht DROP CONSTRAINT FK_Reserveringvlucht_Vlucht;
ALTER TABLE Vluchtboeking.Reserveringvlucht DROP CONSTRAINT FK_Reserveringvlucht_Reservering;
ALTER TABLE Vluchtboeking.Vlucht DROP CONSTRAINT FK_Vlucht_Toestel;
ALTER TABLE Vluchtboeking.Vlucht DROP CONSTRAINT FK_Vlucht_Luchthaven;
ALTER TABLE Vluchtboeking.Vlucht DROP CONSTRAINT FK_Vlucht_Luchthaven;
ALTER TABLE Vluchtboeking.ToestelKlasse DROP CONSTRAINT ToestelKlasse_Klasse;
ALTER TABLE Vluchtboeking.ToestelKlasse DROP CONSTRAINT ToestelKlasse_Toestel;
ALTER TABLE Vluchtboeking.ToestelStoel DROP CONSTRAINT ToestelStoel_Toestel;
ALTER TABLE Vluchtboeking.ToestelStoel DROP CONSTRAINT ToestelStoel_SToel;
GO



DROP TABLE	Vluchtboeking.Passagier;
DROP TABLE	Vluchtboeking.Reservering;
DROP TABLE	Vluchtboeking.Reserveringvlucht;
DROP TABLE	Vluchtboeking.Vlucht;
DROP TABLE	Vluchtboeking.Luchthaven;
DROP TABLE	Vluchtboeking.Toestel;
DROP TABLE	Vluchtboeking.Klasse;
DROP TABLE	Vluchtboeking.Stoel;
DROP TABLE	Vluchtboeking.ToestelKlasse;
DROP TABLE	Vluchtboeking.ToestelStoel;
GO

DROP SCHEMA Vluchtboeking;
GO
CREATE SCHEMA Vluchtboeking;
GO

CREATE TABLE	Vluchtboeking.Passagier
(
	id					INT			NOT NULL,
	emailadres			NVARCHAR(80)	NOT NULL,
	voornaam			NVARCHAR(45)	NOT NULL,
	achternaam			NVARCHAR(45)	NOT NULL,
	telefoonnummer		NVARCHAR(30)	NOT NULL,
	geboortedatum		Date		NOT NULL,
	nationaliteit		NVARCHAR(30) NULL,
	postcode			NVARCHAR(6) NULL,
	plaats				NVARCHAR(30) NULL,		
	straat				NVARCHAR(30) NULL,
	huisnummer			NVARCHAR(5) NULL,
	CONSTRAINT		PK_Passagier	PRIMARY KEY (id),
	CONSTRAINT	UK_Passagier_emailadres
		UNIQUE	(emailadres)
);

CREATE TABLE	Vluchtboeking.Klasse
(
	id					INT			NOT NULL,
	klasseType			NVARCHAR(45)		NOT NULL,
	CONSTRAINT		PK_Klasse	PRIMARY KEY (id)
);

CREATE TABLE	Vluchtboeking.Reservering
(
	id					INT			NOT NULL,
	boekingscode		NVARCHAR(30)		NOT NULL,
	passagierId			INT			NOT NULL,
    klasseId			INT			NOT NULL,
	boekingsdatum		Date		NOT NULL,	
	betaalmethode		NVARCHAR(20)	NOT NULL,
	boekingsstatus		NVARCHAR(30) NULL,
	CONSTRAINT		PK_Reservering	PRIMARY KEY (id),
	CONSTRAINT		FK_Reservering_Passagier
		FOREIGN KEY	(PassagierId)
		REFERENCES Vluchtboeking.Passagier (id),
    CONSTRAINT		FK_Reservering_Klasse
		FOREIGN KEY	(klasseId)
		REFERENCES Vluchtboeking.Klasse(id)
);






CREATE TABLE	Vluchtboeking.Luchthaven
(
	id					INT			NOT NULL,
	IATA				NVARCHAR(4)	NOT NULL,
	stad				NVARCHAR(45)	NOT NULL,
	land				NVARCHAR(45)	NOT NULL,
	CONSTRAINT		PK_Luchthaven	PRIMARY KEY (id),
	CONSTRAINT	UK_Luchthaven_IATA
		UNIQUE	(IATA)
);

CREATE TABLE	Vluchtboeking.Toestel
(
	id					INT			NOT NULL,
	toestelType			NVARCHAR(50)	NOT NULL,
	CONSTRAINT		PK_Toestel	PRIMARY KEY (id)
);

CREATE TABLE	Vluchtboeking.Vlucht
(
	id					NVARCHAR(10)			NOT NULL,
	toestelId			INT			NOT NULL,
	bestemming			INT		    NOT NULL FOREIGN KEY REFERENCES Vluchtboeking.Luchthaven(id),
	vertrek				INT		    NOT NULL FOREIGN KEY REFERENCES Vluchtboeking.Luchthaven(id),
	vertrektijd			DATETIME		NOT NULL,
	aankomsttijd		DATETIME		NOT NULL,
	vluchtprijs			MONEY			NOT NULL,
	datum				Date		NOT NULL,
	CONSTRAINT		PK_Vlucht	PRIMARY KEY (id),
	CONSTRAINT		FK_Vlucht_Toestel
		FOREIGN KEY	(toestelId)
		REFERENCES Vluchtboeking.Toestel(id)
);


CREATE TABLE	Vluchtboeking.Reserveringvlucht
(
	id					INT			NOT NULL,
	reserveringId		INT			NOT NULL,
	vluchtId			NVARCHAR(10)			NOT NULL,
	CONSTRAINT		PK_Reserveringvlucht	PRIMARY KEY (id),
	CONSTRAINT		FK_Reserveringvlucht_Reservering
		FOREIGN KEY	(reserveringId)
		REFERENCES Vluchtboeking.Reservering (id),
	CONSTRAINT		FK_Reserveringvlucht_Vlucht
		FOREIGN KEY	(vluchtId)
		REFERENCES Vluchtboeking.Vlucht (id)
);




CREATE TABLE	Vluchtboeking.ToestelKlasse
(
	id					INT			NOT NULL,
	toestelId			INT			NOT NULL,
	klasseId			INT			NOT NULL,
    klasseprijs         MONEY		    NULL,
	CONSTRAINT		PK_ToestelKlasse	PRIMARY KEY (id),
	CONSTRAINT		FK_ToestelKlasse_Toestel
		FOREIGN KEY	(toestelId)
		REFERENCES Vluchtboeking.Toestel (id),
	CONSTRAINT		FK_ToestelKlasse_Klasse
		FOREIGN KEY	(klasseId)
		REFERENCES Vluchtboeking.Klasse (id)
);
CREATE  TABLE   Vluchtboeking.Stoel
(
    id					INT			   NOT NULL,
    stoelType           NVARCHAR(100)    NOT NULL,
    CONSTRAINT		PK_Stoel	PRIMARY KEY (id)
);
CREATE  TABLE   Vluchtboeking.ToestelStoel
(
    id					INT			   NOT NULL,
    toestelid			INT			   NOT NULL,
    stoelid			    INT			   NOT NULL,
    aantalStoelen		    INT			   NOT NULL,
    stoelprijs			MONEY		     NULL,
    CONSTRAINT		PK_ToestelStoel	PRIMARY KEY (id),
	CONSTRAINT		FK_ToestelStoel_Toestel
		FOREIGN KEY	(toestelId)
		REFERENCES Vluchtboeking.Toestel (id),
	CONSTRAINT		FK_ToestelStoel_Stoel
		FOREIGN KEY	(stoelId)
		REFERENCES Vluchtboeking.Stoel (id)
);

insert into Vluchtboeking.Passagier (id, emailadres, voornaam, achternaam, telefoonnummer, geboortedatum, nationaliteit, postcode, plaats , straat, huisnummer) values (1, 'blije.olga@hotmail.com', 'Olga', 'Blijer', '+597 87 21 247', '1949-04-09', 'Nederlandse', '3293', 'Den Haag', 'van der kampstraat', 7);
insert into Vluchtboeking.Passagier (id, emailadres, voornaam, achternaam, telefoonnummer, geboortedatum, nationaliteit, postcode, plaats , straat, huisnummer) values (2, 'venlo.mj@hotmail.nl', 'Murrel', 'Venlo','+32 474 24 48 07', '1992-03-10', 'Surinaamse', '2430', 'Laakdal','Oude Tramlijn', 3);
insert into Vluchtboeking.Passagier (id, emailadres, voornaam, achternaam, telefoonnummer, geboortedatum, nationaliteit, postcode, plaats , straat, huisnummer) values (3, 'prijor.k@skynet.be','Karel', 'Prijor','+32 474 00 00 07','1962-01-02', 'Belgische', '2400','Tout Lui Faut', 'Sheonathweg', 34);
insert into Vluchtboeking.Passagier (id, emailadres, voornaam, achternaam, telefoonnummer, geboortedatum, nationaliteit, postcode, plaats , straat, huisnummer) values (4, 'cher.vg@gmail.com','Cherijlle', 'Roberts','+597 774  00 07','1975-07-12', 'Surinaamse', '3224','Houttuin', 'Krokikilaan', 16);
insert into Vluchtboeking.Passagier (id, emailadres, voornaam, achternaam, telefoonnummer, geboortedatum, nationaliteit, postcode, plaats , straat, huisnummer) values (5, 'dwight.d@hotmail.be','David', 'Eersteling','+597 871  01 27','1975-07-12', 'Belgische', '3293','Kaggevinne', 'Antwerpseweg', 5);

insert into Vluchtboeking.Klasse (id, klasseType) values (2, 'Economy Flex');
insert into Vluchtboeking.Klasse (id, klasseType) values (1, 'Economy Class');
insert into Vluchtboeking.Klasse (id, klasseType) values (3, 'Business Class');

insert into Vluchtboeking.Reservering (id, boekingscode, passagierId, klasseId, boekingsdatum, betaalmethode, boekingsstatus) values (4, 'RBZT4T', 3, 3, '2021-03-21', 'Mastercard', 'succesvol');
insert into Vluchtboeking.Reservering (id, boekingscode, passagierId, klasseId, boekingsdatum, betaalmethode, boekingsstatus) values (7, 'K6YFDW', 1, 2, '2021-03-03', 'Bankoverschrijving', 'in behandeling');
insert into Vluchtboeking.Reservering (id, boekingscode, passagierId, klasseId, boekingsdatum, betaalmethode, boekingsstatus) values (10, 'NHANQE', 2, 1, '2021-03-11', 'Contant', 'geweigerd');

insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (1, 'PBM', 'Paramaribo', 'Suriname');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (2, 'MAD', 'Madrid', 'Spanje');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (3, 'CGN', 'Keulen', 'Duitsland');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (4, 'AMS', 'Amsterdam', 'Nederland');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (5, 'BCN', 'Barcelona', 'Spanje');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (6, 'BRU', 'Brussel', 'België');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (7, 'FUE', 'Fuerteventura', 'Spanje');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (8, 'PRG', 'Praag', 'Tsjechië');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (9, 'ORY', 'Orly', 'Frankrijk');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (10, 'AUA', 'Oranjestad', 'Aruba');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (11, 'BKK', 'Bangkok', 'Thailand');
insert into Vluchtboeking.Luchthaven (id, IATA, stad, land) values (12, 'EIN', 'Einhoven', 'Nederland');

insert into Vluchtboeking.Toestel (id, toestelType) values (1, 'Boeing 787-10 Dreamliner');
insert into Vluchtboeking.Toestel (id, toestelType) values (2, 'Airbus A320');
insert into Vluchtboeking.Toestel (id, toestelType) values (3, 'Boeing 737-700');
insert into Vluchtboeking.Toestel (id, toestelType) values (4, 'Boeing 787-9');
insert into Vluchtboeking.Toestel (id, toestelType) values (5, 'Airbus A330-300');
insert into Vluchtboeking.Toestel (id, toestelType) values (6, 'Boeing 737-800');
insert into Vluchtboeking.Toestel (id, toestelType) values (7, 'Embraer 190');

insert into Vluchtboeking.Vlucht (id, toestelId, bestemming, vertrek, vertrektijd, aankomsttijd, vluchtprijs, datum) values ('KL713', 3,  1, 4, '10:50:00', '15:10:00', 800.00, '2021-07-30');
insert into Vluchtboeking.Vlucht (id, toestelId, bestemming, vertrek, vertrektijd, aankomsttijd, vluchtprijs, datum) values ('SN3703', 2, 7, 6, '12:30:00', '15:50:00', 146.34, '2021-08-03');
insert into Vluchtboeking.Vlucht (id, toestelId, bestemming, vertrek, vertrektijd, aankomsttijd, vluchtprijs, datum) values ('KL779', 4, 10, 4, '10:50:00', '13:50:00', 750.00, '2021-09-25');
insert into Vluchtboeking.Vlucht (id, toestelId, bestemming, vertrek, vertrektijd, aankomsttijd, vluchtprijs, datum) values ('LX180', 1, 11, 3, '14:30:00', '05:30:00', 745.00, '2021-10-26');
insert into Vluchtboeking.Vlucht (id, toestelId, bestemming, vertrek, vertrektijd, aankomsttijd, vluchtprijs, datum) values ('SN2812', 5, 8, 6, '09:45:00', '11:45:00', 249.99, '2021-11-12');
insert into Vluchtboeking.Vlucht (id, toestelId, bestemming, vertrek, vertrektijd, aankomsttijd, vluchtprijs, datum) values ('HV1324', 6, 9, 12, '08:30:00', '9:30:00', 212.00, '2021-10-08');

insert into Vluchtboeking.Reserveringvlucht (id, reserveringId, vluchtId) values (1, 10, 'KL713');
insert into Vluchtboeking.Reserveringvlucht (id, reserveringId, vluchtId) values (2, 4, 'SN3703');
insert into Vluchtboeking.Reserveringvlucht (id, reserveringId, vluchtId) values (3, 7, 'KL779');


insert into Vluchtboeking.ToestelKlasse (id, toestelId, klasseId, klasseprijs) values (1, 1, 1, 420.00);
insert into Vluchtboeking.ToestelKlasse (id, toestelId, klasseId, klasseprijs) values (2, 3, 2, null);
insert into Vluchtboeking.ToestelKlasse (id, toestelId, klasseId, klasseprijs) values (3, 2, 3, 654.00);

insert into Vluchtboeking.Stoel(id, stoelType) values (1, 'standaardstoel');
insert into Vluchtboeking.Stoel(id, stoelType) values (2, 'Comfort stoel');
insert into Vluchtboeking.Stoel(id, stoelType) values (3, 'Businessclassstoel');
insert into Vluchtboeking.Stoel(id, stoelType) values (4, 'Stoel met extra beenruimte');
insert into Vluchtboeking.Stoel(id, stoelType) values (5, 'Stoel in de voorsectie');

insert into Vluchtboeking.ToestelStoel(id, toestelId, stoelId, aantalStoelen, stoelprijs) values (1, 1, 1, 344, null);
insert into Vluchtboeking.ToestelStoel(id, toestelId, stoelId, aantalStoelen, stoelprijs) values (2, 1, 2, 344, 146.00);
insert into Vluchtboeking.ToestelStoel(id, toestelId, stoelId, aantalStoelen, stoelprijs) values (3, 1, 3, 344, 0.00);
insert into Vluchtboeking.ToestelStoel(id, toestelId, stoelId, aantalStoelen, stoelprijs) values (4, 1, 5, 344, 40.00);
insert into Vluchtboeking.ToestelStoel(id, toestelId, stoelId, aantalStoelen, stoelprijs) values (5, 1, 5, 344, 58.00);
insert into Vluchtboeking.ToestelStoel(id, toestelId, stoelId, aantalStoelen, stoelprijs) values (6, 2, 1, 180, null);
insert into Vluchtboeking.ToestelStoel(id, toestelId, stoelId, aantalStoelen, stoelprijs) values (7, 3, 2, 149, null);
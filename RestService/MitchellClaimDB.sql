DROP TABLE VehicleList CASCADE;
DROP TABLE VehicleInfo CASCADE;
DROP TABLE CauseOfLossCode CASCADE;
DROP TABLE LossInfo CASCADE;
DROP TABLE StatusCode CASCADE;
DROP TABLE MitchellClaims CASCADE;

CREATE TABLE VehicleList
(
	VListID SERIAL PRIMARY KEY,
	VListClaimNumber varchar(50)
);

CREATE TABLE VehicleInfo
(
	ListID int REFERENCES VehicleList(VListID) ON DELETE CASCADE,
	ModelYear int NOT NULL,
	MakeDescription varchar(100),
	ModelDescription varchar(100),
	EngineDescription varchar(100),
	ExteriorColor varchar(50),
	Vin varchar(50),
	LicPlate varchar(50),
	LicPlateState varchar(50),
	LicPlateExpDate date,
	DamageDescription varchar(500),
	Mileage int
);

CREATE TABLE CauseOfLossCode
(
	CauseID SERIAL PRIMARY KEY,
	CauseDesc varchar(50)
);
INSERT INTO CauseOfLossCode(CauseDesc) VALUES ('Collision'),('Explosion'),('Fire'),('Hail'),('Mechanical Breakdown'),('Other');

CREATE TABLE LossInfo
(
	LossInfoID SERIAL PRIMARY KEY,
	CauseOfLoss int REFERENCES CauseOfLossCode(CauseID),
	ReportedDate timestamp,
	LossDescription varchar(500)
);

CREATE TABLE StatusCode
(
	StatusID SERIAL PRIMARY KEY,
	StatusDesc varchar(50)
);
INSERT INTO StatusCode(StatusDesc) VALUES ('OPEN'),('CLOSED');

CREATE TABLE MitchellClaims
(
	ClaimNumber varchar(50) PRIMARY KEY,
	ClaimantFirstName varchar(50),
	ClaimantLastName varchar(50),
	Status int REFERENCES StatusCode(StatusID),
	LossDate timestamp,
	LossInfo int REFERENCES LossInfo(LossInfoID) ON DELETE CASCADE,
	AssignedAdjusterID bigint,
	Vehicles int REFERENCES VehicleList(VListID)
);

--ALTER TABLE MitchellClaims ADD CONSTRAINT LossInfo_fkey FOREIGN KEY (LossInfo) REFERENCES LossInfo(LossInfoID) ON DELETE CASCADE;
--ALTER TABLE VehicleList ADD CONSTRAINT Claim_fkey FOREIGN KEY (ClaimNumber) REFERENCES MitchellClaims(ClaimNumberMain) ON DELETE CASCADE;

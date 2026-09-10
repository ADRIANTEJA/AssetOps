BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Account" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL UNIQUE,
	"initialBalance"	REAL NOT NULL,
	"currentBalance"	REAL NOT NULL,
	"selectionStatus"	INTEGER NOT NULL DEFAULT 0 CHECK("selectionStatus" = 0 OR "selectionStatus" = 1),
	"bankruptcyStatus"	INTEGER NOT NULL DEFAULT 0 CHECK("bankruptcyStatus" = 0 OR "bankruptcyStatus" = 1),
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "AnalysisNote" (
	"id"	INTEGER NOT NULL,
	"strategyId"	INTEGER NOT NULL,
	"title"	TEXT NOT NULL,
	"text"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("strategyId") REFERENCES "Strategy"("id") ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Performance" (
	"accountId"	INTEGER NOT NULL,
	"date"	REAL NOT NULL,
	"roi"	REAL NOT NULL,
	"roiPercentage"	REAL,
	"cost"	REAL NOT NULL,
	FOREIGN KEY("accountId") REFERENCES "Account"("id")
);
CREATE TABLE IF NOT EXISTS "Strategy" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL UNIQUE,
	"goal"	TEXT,
	"intermediary"	TEXT,
	"riskRewardRatio"	TEXT NOT NULL,
	"maxTradeRisk"	REAL NOT NULL,
	"dailyGoal"	REAL NOT NULL,
	"maxDailyLoss"	REAL NOT NULL,
	"wins"	INTEGER NOT NULL DEFAULT 0,
	"losses"	INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Symbol" (
	"id"	INTEGER NOT NULL,
	"pair"	TEXT NOT NULL UNIQUE,
	"assetType"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Trade" (
	"id"	INTEGER NOT NULL UNIQUE,
	"accountId"	INTEGER NOT NULL,
	"pairTraded"	TEXT NOT NULL,
	"pairMarket"	TEXT NOT NULL,
	"openDate"	REAL NOT NULL,
	"closeDate"	REAL,
	"side"	INTEGER NOT NULL DEFAULT 1 CHECK("side" = 0 OR "side" = 1),
	"volume"	REAL,
	"status"	INTEGER NOT NULL DEFAULT 1 CHECK("status" = 0 OR "status" = 1),
	"openPrice"	REAL NOT NULL,
	"closePrice"	REAL,
	"tradeCost"	REAL NOT NULL,
	"swap"	REAL NOT NULL DEFAULT 0,
	"spread"	REAL NOT NULL DEFAULT 0,
	"commission"	REAL NOT NULL DEFAULT 0,
	"otherCosts"	REAL NOT NULL DEFAULT 0,
	"takeProfit"	REAL,
	"stopLoss"	REAL,
	"roi"	REAL,
	"roiPercentage"	REAL,
	"mistakes"	TEXT,
	"notes"	TEXT,
	"strategyName"	TEXT,
	"leverage"	INTEGER DEFAULT 1,
	"accountBalance"	REAL,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("accountId") REFERENCES "Account"("id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "TradeImage" (
	"id"	INTEGER NOT NULL,
	"tradeId"	INTEGER NOT NULL,
	"image"	BLOB NOT NULL,
	FOREIGN KEY("tradeId") REFERENCES "Trade"("id") ON UPDATE CASCADE ON DELETE CASCADE
);
COMMIT;

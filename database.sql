CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "users" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_users" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "password_hash" TEXT NOT NULL
);

CREATE TABLE "wallets" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_wallets" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NULL,
    "currency" TEXT NOT NULL,
    "user_id" INTEGER NOT NULL,
    "amount" TEXT NULL,
    CONSTRAINT "FK_wallets_users_user_id" FOREIGN KEY ("user_id") REFERENCES "users" ("id") ON DELETE CASCADE
);

CREATE TABLE "operations" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_operations" PRIMARY KEY AUTOINCREMENT,
    "wallet_id" INTEGER NOT NULL,
    "date" TEXT NOT NULL,
    "value" TEXT NOT NULL,
    "operation_type" TEXT NOT NULL,
    "expense_type" TEXT NULL,
    "income_type" TEXT NULL,
    CONSTRAINT "FK_operations_wallets_wallet_id" FOREIGN KEY ("wallet_id") REFERENCES "wallets" ("id") ON DELETE CASCADE
);

CREATE INDEX "IX_operations_wallet_id" ON "operations" ("wallet_id");

CREATE UNIQUE INDEX "idx_user_email" ON "users" ("email");

CREATE INDEX "IX_wallets_user_id" ON "wallets" ("user_id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221127160847_InitialDatabaseSchema', '6.0.10');

COMMIT;


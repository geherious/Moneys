-- +goose Up
-- +goose StatementBegin
CREATE TABLE users (
  id            BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  username      TEXT NOT NULL UNIQUE,
  email         TEXT NOT NULL UNIQUE,
  password      TEXT NOT NULL,
  registered_at TIMESTAMP NOT NULL
);

CREATE TABLE refresh_tokens (
  user_id     BIGINT REFERENCES users(id),
  hash        TEXT,
  expires_at  TIMESTAMP NOT NULL,
  PRIMARY KEY (hash, user_id)
);

CREATE TABLE accounts (
  id           BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  name         TEXT NOT NULL,
  is_private   BOOLEAN NOT NULL,
  owned_by     BIGINT REFERENCES users(id) NOT NULL,
  has_balances BOOLEAN NOT NULL
);

CREATE TABLE accounts_to_users (
  account_id BIGINT REFERENCES accounts(id),
  user_id    BIGINT REFERENCES users(id),
  PRIMARY KEY (user_id, account_id)
);

CREATE TABLE categories (
  id         BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  name       TEXT NOT NULL,
  parent_id  BIGINT REFERENCES categories(id),
  color      TEXT NOT NULL,
  account_id BIGINT REFERENCES accounts(id) NOT NULL,
  is_profit  BOOLEAN NOT NULL
);

CREATE TABLE expenses (
  id          BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  description TEXT,
  category_id BIGINT REFERENCES categories(id) NOT NULL,
  amount      NUMERIC(11, 2) NOT NULL,
  paid_by     BIGINT REFERENCES users(id) NOT NULL
);

CREATE TABLE expenses_splits (
  expense_id  BIGINT REFERENCES expenses(id),
  user_id     BIGINT REFERENCES users(id),
  percent     NUMERIC(5, 2) NOT NULL,
  PRIMARY KEY (expense_id, user_id)
);

CREATE TABLE share_policies (
  id         BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  account_id BIGINT REFERENCES accounts(id) NOT NULL,
  name       TEXT NOT NULL
);

CREATE TABLE share_policy_items (
  policy_id   BIGINT REFERENCES share_policies(id),
  user_id     BIGINT REFERENCES users(id),
  percent     NUMERIC(5, 2) NOT NULL,
  PRIMARY KEY (user_id, policy_id)
);

CREATE TABLE user_split_balances (
  id          BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  account_id  BIGINT REFERENCES accounts(id) NOT NULL,
  user_from   BIGINT REFERENCES users(id) NOT NULL,
  user_to     BIGINT REFERENCES users(id) NOT NULL,
  amount      NUMERIC(11, 2) NOT NULL
);

CREATE TABLE balances (
  id         BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  name       TEXT NOT NULL,
  user_id    BIGINT REFERENCES users(id),
  account_id BIGINT REFERENCES accounts(id),
  amount     NUMERIC(11, 2) NOT NULL
);

CREATE TABLE transactions (
  id           BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  balance_from BIGINT REFERENCES balances(id) NOT NULL,
  balance_to   BIGINT REFERENCES balances(id) NOT NULL,
  created_by   BIGINT REFERENCES users(id) NOT NULL,
  created_at   TIMESTAMP NOT NULL,
  amount       NUMERIC(11, 2) NOT NULL
);
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
DROP TABLE IF EXISTS transactions;

DROP TABLE IF EXISTS balances;

DROP TABLE IF EXISTS user_split_balances;

DROP TABLE IF EXISTS share_policy_items;

DROP TABLE IF EXISTS share_policies;

DROP TABLE IF EXISTS expenses_splits;

DROP TABLE IF EXISTS expenses;

DROP TABLE IF EXISTS categories;

DROP TABLE IF EXISTS accounts_to_users;

DROP TABLE IF EXISTS accounts;

DROP TABLE IF EXISTS refresh_tokens;

DROP TABLE IF EXISTS users;
-- +goose StatementEnd

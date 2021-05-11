-- 1. Добавить внешние ключи
ALTER TABLE dealer
    ADD CONSTRAINT FK_dealer_company FOREIGN KEY (id_company) REFERENCES company (id_company);
ALTER TABLE "order"
    ADD CONSTRAINT FK_order_production FOREIGN KEY (id_production) REFERENCES "production" (id_production);
ALTER TABLE "order"
    ADD CONSTRAINT FK_order_dealer FOREIGN KEY (id_dealer) REFERENCES "dealer" (id_dealer);
ALTER TABLE "order"
    ADD CONSTRAINT FK_order_pharmacy FOREIGN KEY (id_pharmacy) REFERENCES "pharmacy" (id_pharmacy);
ALTER TABLE "production"
    ADD CONSTRAINT FK_production_company FOREIGN KEY (id_company) REFERENCES "company" (id_company);
ALTER TABLE "production"
    ADD CONSTRAINT FK_production_medicine FOREIGN KEY (id_medicine) REFERENCES "medicine" (id_medicine);

-- 2. Выдать информацию по всем заказам лекарства “Кордерон” компании
-- “Аргус” с указанием названий аптек, дат, объема заказов.
SELECT "order".id_order, pharmacy.name, "order".date, "order".quantity
FROM (SELECT id_production FROM production WHERE (id_company = 7) AND (id_medicine = 10)) as pip
         JOIN "order" ON "order".id_production = pip.id_production
         JOIN pharmacy ON pharmacy.id_pharmacy = "order".id_pharmacy;

-- 3. Дать список лекарств компании “Фарма”, на которые не были сделаны
-- заказы до 25 января.
SELECT *
FROM medicine
WHERE id_medicine NOT IN (
    SELECT production.id_medicine
    FROM "order"
             JOIN production on production.id_production = "order".id_production
    WHERE (production.id_company = 8)
      AND ("order".date < '2019-01-25')
);

-- 4. Дать минимальный и максимальный баллы лекарств каждой фирмы,
-- которая оформила не менее 120 заказов
SELECT MIN(production.rating), MAX(production.rating), production.id_company
FROM "order"
         JOIN production ON "order".id_production = production.id_production
GROUP BY production.id_company
HAVING COUNT(*) > 120;

-- 5. Дать списки сделавших заказы аптек по всем дилерам компании
-- “AstraZeneca”. Если у дилера нет заказов, в названии аптеки проставить NULL.
SELECT DISTINCT dealer.name, pharmacy.name
FROM dealer
         LEFT JOIN "order" on dealer.id_dealer = "order".id_dealer
         LEFT JOIN pharmacy on "order".id_pharmacy = pharmacy.id_pharmacy
WHERE dealer.id_company = 4;

-- 6. Уменьшить на 20% стоимость всех лекарств, если она превышает 3000,
-- а длительность лечения не более 7 дней.
UPDATE production
SET price = 0.8 * price
FROM medicine
WHERE (production.price > 3000)
  AND (medicine.cure_duration <= 7);

-- 7. Добавить необходимые индексы.

CREATE UNIQUE INDEX IX_medicine_name ON medicine (name);
CREATE UNIQUE INDEX IX_production_id_company_id_medicine ON production (id_company, id_medicine);
CREATE UNIQUE INDEX IX_pharmacy_name ON pharmacy (name);
CREATE UNIQUE INDEX IX_dealer_phone ON dealer (phone);
CREATE UNIQUE INDEX IX_company_name ON company (name);

CREATE INDEX IX_order_date ON "order"(date);
CREATE UNIQUE INDEX IX_dealer_name ON dealer (name);

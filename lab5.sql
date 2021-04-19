-- 1. Добавить внешние ключи
ALTER TABLE booking
    ADD CONSTRAINT FK_booking_client FOREIGN KEY (id_client) REFERENCES client (id_client);
ALTER TABLE room
    ADD CONSTRAINT FK_room_hotel FOREIGN KEY (id_hotel) REFERENCES hotel (id_hotel);
ALTER TABLE room
    ADD CONSTRAINT FK_room_room_category FOREIGN KEY (id_room_category) REFERENCES room (id_room);
ALTER TABLE room_in_booking
    ADD CONSTRAINT FK_room_in_booking_booking FOREIGN KEY (id_booking) REFERENCES booking (id_booking);
ALTER TABLE room_in_booking
    ADD CONSTRAINT FK_room_in_booking_room FOREIGN KEY (id_room) REFERENCES room (id_room);

-- 2. Выдать информацию о клиентах гостиницы “Космос”, проживающих в номерах категории “Люкс” на 1 апреля 2019г.
SELECT client.id_client, client.name, client.phone
FROM room_in_booking
         JOIN booking ON room_in_booking.id_booking = booking.id_booking
         JOIN room ON room_in_booking.id_room = room.id_room
         JOIN client ON booking.id_client = client.id_client
WHERE (room_in_booking.checkin_date <= '2019-04-01')
  AND (room_in_booking.checkout_date > '2019-04-01')
  AND (room.id_hotel = 1)
  AND (room.id_room_category = 5);

-- 3. Дать список свободных номеров всех гостиниц на 22 апреля.
SELECT room.id_room, room.id_hotel
FROM room
         LEFT JOIN room_in_booking ON room.id_room = room_in_booking.id_room
WHERE (room_in_booking.checkin_date IS NULL)
   OR (room_in_booking.checkin_date > '2019-04-22')
   OR (room_in_booking.checkout_date <= '2019-04-22')
GROUP BY (room.id_room, room.id_hotel);

-- 4. Дать количество проживающих в гостинице “Космос” на 23 марта по каждой категории номеров
SELECT id_room_category, COUNT(*)
FROM room_in_booking
         JOIN room ON room_in_booking.id_room = room.id_room
WHERE (room.id_hotel = 1)
  AND (room_in_booking.checkin_date <= '2019-03-23')
  AND (room_in_booking.checkout_date > '2019-03-23')
GROUP BY room.id_room_category;

-- 5. Дать список последних проживавших клиентов по всем комнатам гостиницы “Космос”,
-- выехавшим в апреле с указанием даты выезда.
SELECT client.name, room.id_room, room.id_hotel, room_in_booking.checkout_date
FROM room_in_booking
         JOIN booking ON room_in_booking.id_booking = booking.id_booking
         JOIN client ON booking.id_client = client.id_client
         JOIN room ON room_in_booking.id_room = room.id_room
WHERE (room_in_booking.id_room, room_in_booking.checkout_date) IN
      (SELECT room_in_booking.id_room, MAX(room_in_booking.checkout_date) AS date
       FROM room_in_booking
       WHERE EXTRACT(MONTH FROM room_in_booking.checkout_date) = 4
       GROUP BY(room_in_booking.id_room))
  AND (room.id_hotel = 1);

-- 6. Продлить на 2 дня дату проживания в гостинице “Космос” всем клиентам комнат
-- категории “Бизнес”, которые заселились 10 мая.
UPDATE room_in_booking
SET checkout_date = checkout_date + INTERVAL '2 DAY'
FROM room
WHERE (room_in_booking.checkin_date = '2019-05-10')
  AND (room.id_room_category = 3)
  AND (room.id_hotel = 1);

-- 7. Найти все "пересекающиеся" варианты проживания
SELECT *
FROM room_in_booking AS rib1
         JOIN room_in_booking AS rib2
              ON (rib1.checkin_date < rib2.checkout_date) AND (rib1.checkout_date > rib2.checkin_date)
                  AND (rib1.id_booking != rib2.id_booking) AND (rib1.id_room = rib2.id_room);

-- 8. Создать бронирование в транзакции.
BEGIN ISOLATION LEVEL REPEATABLE READ;
DO
$$
    DECLARE
        createdBookingId INTEGER;
        bookingDate      DATE    = '2020-01-01';
        checkinDate      DATE    = '2020-01-03';
        checkoutDate     DATE    = '2020-01-03';
        roomId           INTEGER = 1;
        clientId         INTEGER = 1;
    BEGIN
        IF NOT EXISTS(SELECT *
                      FROM room_in_booking AS rib
                      WHERE (rib.checkin_date >= checkinDate) AND (rib.checkout_date < checkoutDate)) THEN
            INSERT INTO booking(id_client, booking_date)
            VALUES (clientId, bookingDate)
            RETURNING id_booking INTO createdBookingId;
            INSERT INTO room_in_booking(id_booking, id_room, checkin_date, checkout_date)
            VALUES (createdBookingId, roomId, checkinDate, checkoutDate);
        END IF;
    end;
$$;
COMMIT;

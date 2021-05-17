-- 1. Добавить внешние ключи.
ALTER TABLE lesson
    ADD CONSTRAINT FK_lesson_teacher FOREIGN KEY (id_teacher) REFERENCES teacher (id_teacher);
ALTER TABLE lesson
    ADD CONSTRAINT FK_lesson_subject FOREIGN KEY (id_subject) REFERENCES subject (id_subject);
ALTER TABLE lesson
    ADD CONSTRAINT FK_lesson_group FOREIGN KEY (id_group) REFERENCES "group" (id_group);
ALTER TABLE mark
    ADD CONSTRAINT FK_mark_lesson FOREIGN KEY (id_lesson) REFERENCES "lesson" (id_lesson);
ALTER TABLE mark
    ADD CONSTRAINT FK_mark_student FOREIGN KEY (id_student) REFERENCES "student" (id_student);
ALTER TABLE student
    ADD CONSTRAINT FK_student_group FOREIGN KEY (id_group) REFERENCES "group" (id_group);

-- 2. Выдать оценки студентов по информатике если они обучаются данному предмету.
-- Оформить выдачу данных с использованием view.
CREATE VIEW cs_marks AS
SELECT student.id_student as StudentId, student.name as Name, mark.mark as Mark
FROM mark
         JOIN lesson ON mark.id_lesson = lesson.id_lesson
         JOIN student ON student.id_student = mark.id_student
WHERE lesson.id_subject = 2;

-- 3. Дать информацию о должниках с указанием фамилии студента и названия предмета.
-- Должниками считаются студенты, не имеющие оценки по предмету, который ведется в
-- группе. Оформить в виде процедуры, на входе идентификатор группы.
CREATE OR REPLACE FUNCTION get_debtors(
    selectedGroupId int
)
    RETURNS TABLE
            (
                id_student   INT,
                student_name TEXT,
                subject_name TEXT
            )
    LANGUAGE plpgsql
AS
$$
BEGIN
    RETURN QUERY
        SELECT student.id_student, student.name, subject.name
        FROM lesson
                 JOIN "group" ON "group".id_group = lesson.id_group
                 JOIN subject ON lesson.id_subject = subject.id_subject
                 JOIN student ON "group".id_group = student.id_group
                 LEFT JOIN mark ON lesson.id_lesson = mark.id_lesson
        WHERE (student.id_group = selectedGroupId)
          AND (mark.id_mark IS NULL);
END
$$;

SELECT *
FROM get_debtors(1);

-- 4.Дать среднюю оценку студентов по каждому предмету для тех
-- предметов, по которым занимается не менее 35 студентов.
SELECT lesson.id_subject, AVG(mark.mark)
FROM mark
         JOIN lesson ON lesson.id_lesson = mark.id_lesson
GROUP BY lesson.id_subject
HAVING COUNT(mark.id_student) >= 35
ORDER BY lesson.id_subject;

-- 5.Дать оценки студентов специальности ВМ по всем проводимым
-- предметам с указанием группы, фамилии, предмета, даты.
-- При отсутствии оценки заполнить значениями NULL поля оценки
SELECT "group".name, student.name, subject.name, lesson.date, mark.mark FROM student
    JOIN "group" on "group".id_group = student.id_group
    JOIN lesson on "group".id_group = lesson.id_group
    JOIN subject on subject.id_subject = lesson.id_subject
    LEFT JOIN mark on student.id_student = mark.id_student
    WHERE "group".id_group = 3;

-- 6.Всем студентам специальности ПС, получившим оценки меньшие
-- 5 по предмету БД до 12.05, повысить эти оценки на 1 балл.
UPDATE mark
SET mark = mark + 1
FROM student, lesson
WHERE (student.id_group = 1)
  AND (mark.mark != 5)
  AND (lesson.date < '2019-05-12');

-- 7.Добавить необходимые индексы.
CREATE UNIQUE INDEX IX_teacher_phone ON teacher (phone);
CREATE UNIQUE INDEX IX_subject_name ON subject (name);
CREATE UNIQUE INDEX IX_group_name ON "group" (name);
CREATE UNIQUE INDEX IX_student_phone ON student (phone);
CREATE UNIQUE INDEX IX_student_name ON student (name);
CREATE UNIQUE INDEX IX_teacher_name ON teacher (name);

CREATE INDEX IX_lesson_id_subject ON lesson (id_subject);
CREATE INDEX IX_lesson_date ON lesson (date);
CREATE INDEX IX_student_id_group ON student (id_group);
CREATE INDEX IX_mark_id_student ON mark (id_student);

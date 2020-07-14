create table phonebook
(
    id         int auto_increment
        primary key,
    first_name varchar(50) not null,
    last_name  varchar(50) not null,
    number     varchar(20) not null
);

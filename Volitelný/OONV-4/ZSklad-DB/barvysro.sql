-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Počítač: 127.0.0.1
-- Vytvořeno: Pát 05. zář 2025, 23:54
-- Verze serveru: 10.4.28-MariaDB
-- Verze PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Databáze: `barvysro`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `objednavky`
--

CREATE TABLE `objednavky` (
  `ID` int(11) NOT NULL,
  `ID_vyrizujici` int(11) NOT NULL,
  `ID_zakaznik` int(11) NOT NULL,
  `datum_zalozeni` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `objednavky`
--

INSERT INTO `objednavky` (`ID`, `ID_vyrizujici`, `ID_zakaznik`, `datum_zalozeni`) VALUES
(1, 1, 1, '2024-01-01 10:00:00'),
(2, 2, 2, '2024-01-02 11:30:00'),
(3, 3, 3, '2024-01-03 12:45:00'),
(4, 4, 4, '2024-01-04 13:15:00'),
(5, 5, 5, '2024-01-05 14:20:00'),
(6, 6, 6, '2024-01-06 15:00:00'),
(7, 7, 7, '2024-01-07 16:30:00'),
(8, 8, 8, '2024-01-08 17:45:00'),
(9, 9, 9, '2024-01-09 18:50:00'),
(10, 10, 10, '2024-01-10 19:00:00'),
(11, 11, 11, '2024-01-11 09:10:00'),
(12, 12, 12, '2024-01-12 10:30:00'),
(13, 13, 13, '2024-01-13 11:20:00'),
(14, 14, 14, '2024-01-14 12:45:00'),
(15, 15, 15, '2024-01-15 13:50:00'),
(16, 16, 16, '2024-01-16 14:00:00'),
(17, 17, 17, '2024-01-17 15:30:00'),
(18, 18, 18, '2024-01-18 16:15:00'),
(19, 19, 19, '2024-01-19 17:00:00'),
(20, 20, 20, '2024-01-20 18:30:00'),
(21, 21, 21, '2024-01-21 19:45:00'),
(22, 22, 22, '2024-01-22 20:50:00'),
(23, 23, 23, '2024-01-23 09:00:00'),
(24, 24, 24, '2024-01-24 10:30:00'),
(25, 25, 25, '2024-01-25 11:45:00'),
(26, 26, 26, '2024-01-26 12:50:00'),
(27, 27, 27, '2024-01-27 13:00:00'),
(28, 28, 28, '2024-01-28 14:30:00'),
(29, 29, 29, '2024-01-29 15:45:00'),
(30, 30, 30, '2024-01-30 16:50:00');

-- --------------------------------------------------------

--
-- Zástupná struktura pro pohled `objednavky_produkty_view`
-- (Vlastní pohled viz níže)
--
CREATE TABLE `objednavky_produkty_view` (
`objednavka_id` int(11)
,`zakaznik_jmeno` varchar(40)
,`produkt_nazev` varchar(40)
,`produkt_cena` int(11)
);

-- --------------------------------------------------------

--
-- Struktura tabulky `obsah_objednavky`
--

CREATE TABLE `obsah_objednavky` (
  `ID_objednavka` int(11) NOT NULL,
  `ID_produkt` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `obsah_objednavky`
--

INSERT INTO `obsah_objednavky` (`ID_objednavka`, `ID_produkt`) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20),
(21, 21),
(22, 22),
(23, 23),
(24, 24),
(25, 25),
(26, 26),
(27, 27),
(28, 28),
(29, 29),
(30, 30);

-- --------------------------------------------------------

--
-- Struktura tabulky `oddeleni`
--

CREATE TABLE `oddeleni` (
  `ID` int(11) NOT NULL,
  `nazev` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `oddeleni`
--

INSERT INTO `oddeleni` (`ID`, `nazev`) VALUES
(1, 'Prodej'),
(2, 'Podpora'),
(3, 'Marketing'),
(4, 'Vývoj'),
(5, 'Administrativa'),
(6, 'Finanční oddělení'),
(7, 'Právní oddělení'),
(8, 'Lidské zdroje'),
(9, 'Logistika'),
(10, 'Výroba'),
(11, 'Údržba'),
(12, 'IT oddělení'),
(13, 'Výzkum a vývoj'),
(14, 'Kvalita'),
(15, 'Bezpečnost'),
(16, 'Ochrana zdraví'),
(17, 'Sklad'),
(18, 'Zákaznický servis'),
(19, 'Vedení'),
(20, 'Projektové řízení'),
(21, 'Vzdělávání'),
(22, 'Komunikace'),
(23, 'Kontrola'),
(24, 'Analýzy'),
(25, 'Plánování'),
(26, 'Nákup'),
(27, 'Strategie'),
(28, 'Procesy'),
(29, 'Produkce'),
(30, 'Testování');

-- --------------------------------------------------------

--
-- Struktura tabulky `pozice`
--

CREATE TABLE `pozice` (
  `ID` int(11) NOT NULL,
  `jmeno` varchar(40) NOT NULL,
  `plat` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `pozice`
--

INSERT INTO `pozice` (`ID`, `jmeno`, `plat`) VALUES
(1, 'Manažer prodeje', 60000),
(2, 'Zákaznická podpora', 40000),
(3, 'Marketingový specialista', 50000),
(4, 'Vývojář', 70000),
(5, 'Administrativní pracovník', 35000),
(6, 'Účetní', 45000),
(7, 'Právník', 80000),
(8, 'HR manažer', 55000),
(9, 'Logistik', 50000),
(10, 'Výrobní pracovník', 30000),
(11, 'Technik údržby', 40000),
(12, 'IT specialista', 65000),
(13, 'Výzkumník', 75000),
(14, 'Kvalitář', 45000),
(15, 'Bezpečnostní pracovník', 35000),
(16, 'Ochránce zdraví', 40000),
(17, 'Skladník', 30000),
(18, 'Servisní technik', 40000),
(19, 'Ředitel', 100000),
(20, 'Projektový manažer', 60000),
(21, 'Školitel', 45000),
(22, 'Komunikační specialista', 50000),
(23, 'Kontrolor', 45000),
(24, 'Analytik', 60000),
(25, 'Plánovač', 50000),
(26, 'Nákupčí', 55000),
(27, 'Strategický manažer', 70000),
(28, 'Procesní inženýr', 65000),
(29, 'Produkční manažer', 60000),
(30, 'Testovací technik', 45000);

-- --------------------------------------------------------

--
-- Struktura tabulky `produkty`
--

CREATE TABLE `produkty` (
  `ID` int(11) NOT NULL,
  `nazev` varchar(40) NOT NULL,
  `cena` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `produkty`
--

INSERT INTO `produkty` (`ID`, `nazev`, `cena`) VALUES
(1, 'Bílá barva', 200),
(2, 'Černá barva', 180),
(3, 'Červená barva', 220),
(4, 'Modrá barva', 210),
(5, 'Zelená barva', 190),
(6, 'Žlutá barva', 200),
(7, 'Hnědá barva', 180),
(8, 'Šedá barva', 210),
(9, 'Oranžová barva', 220),
(10, 'Fialová barva', 190),
(11, 'Růžová barva', 200),
(12, 'Bezbarvý lak', 250),
(13, 'Ochranný nátěr', 280),
(14, 'Barvicí štětec', 50),
(15, 'Rozprašovač barvy', 70),
(16, 'Barvicí váleček', 80),
(17, 'Stěrka na barvu', 60),
(18, 'Maskovací páska', 30),
(19, 'Brusný papír', 20),
(20, 'Vrtačka s míchačkou', 300),
(21, 'Přípravek na odstranění barvy', 150),
(22, 'Barvicí pistole', 400),
(23, 'Barvící šablona', 100),
(24, 'Barvící rukavice', 10),
(25, 'Stojan na barvu', 120),
(26, 'Barvící mixér', 350),
(27, 'Pomocný barvicí stůl', 200),
(28, 'Krycí plátno', 180),
(29, 'Paleta na barvy', 40),
(30, 'Barvicí nádoba', 30);

-- --------------------------------------------------------

--
-- Struktura tabulky `sklad`
--

CREATE TABLE `sklad` (
  `id_produkt` int(11) NOT NULL,
  `pocet_kusu` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `sklad`
--

INSERT INTO `sklad` (`id_produkt`, `pocet_kusu`) VALUES
(1, 100),
(2, 5),
(3, 534),
(4, 12),
(5, 354),
(6, 1),
(7, 71),
(1, 100),
(2, 5),
(3, 534),
(4, 12),
(5, 354),
(6, 1),
(7, 71);

-- --------------------------------------------------------

--
-- Struktura tabulky `uzivatele`
--

CREATE TABLE `uzivatele` (
  `id` int(11) NOT NULL,
  `jmeno` varchar(50) NOT NULL,
  `heslo` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `uzivatele`
--

INSERT INTO `uzivatele` (`id`, `jmeno`, `heslo`) VALUES
(3, 'a', '$2y$10$nSfJEkmD2sP8mzhUiD08o.FcbjOHGUISnnOZl4U0jEdGtTreoEFVK');

-- --------------------------------------------------------

--
-- Struktura tabulky `zakaznici`
--

CREATE TABLE `zakaznici` (
  `ID` int(11) NOT NULL,
  `jmeno` varchar(40) NOT NULL,
  `prijmeni` varchar(40) NOT NULL,
  `kontakt` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `zakaznici`
--

INSERT INTO `zakaznici` (`ID`, `jmeno`, `prijmeni`, `kontakt`) VALUES
(1, 'Jan', 'Novák', 601234567),
(2, 'Petr', 'Svoboda', 602345678),
(3, 'Eva', 'Novotná', 603456789),
(4, 'Jiří', 'Dvořák', 604567890),
(5, 'Marie', 'Černá', 605678901),
(6, 'Karel', 'Procházka', 606789012),
(7, 'Jana', 'Kučerová', 607890123),
(8, 'Pavel', 'Veselý', 608901234),
(9, 'Lenka', 'Němcová', 609012345),
(10, 'Martin', 'Král', 601234568),
(11, 'Hana', 'Pokorná', 602345679),
(12, 'Tomáš', 'Hájek', 603456780),
(13, 'Michaela', 'Králová', 604567891),
(14, 'Michal', 'Kovář', 605678902),
(15, 'Lucie', 'Nováková', 606789013),
(16, 'Václav', 'Zeman', 607890124),
(17, 'Alena', 'Šimková', 608901235),
(18, 'Roman', 'Urban', 609012346),
(19, 'Jitka', 'Kolářová', 601234569),
(20, 'Filip', 'Beneš', 602345670),
(21, 'Zdeněk', 'Fiala', 603456781),
(22, 'Petra', 'Malá', 604567892),
(23, 'Vojtěch', 'Bartoš', 605678903),
(24, 'Barbora', 'Vlčková', 606789014),
(25, 'Jaroslav', 'Holý', 607890125),
(26, 'Klára', 'Jelínková', 608901236),
(27, 'Radek', 'Hruška', 609012347),
(28, 'Martina', 'Sedláčková', 601234570),
(29, 'Lukáš', 'Bláha', 602345671),
(30, 'Anna', 'Růžičková', 603456782);

-- --------------------------------------------------------

--
-- Struktura tabulky `zamestnanci`
--

CREATE TABLE `zamestnanci` (
  `ID` int(11) NOT NULL,
  `jmeno` varchar(40) NOT NULL,
  `prijmeni` varchar(40) NOT NULL,
  `pozice` int(11) NOT NULL,
  `oddeleni` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_czech_ci;

--
-- Vypisuji data pro tabulku `zamestnanci`
--

INSERT INTO `zamestnanci` (`ID`, `jmeno`, `prijmeni`, `pozice`, `oddeleni`) VALUES
(1, 'Jan', 'Novotný', 1, 1),
(2, 'Petr', 'Novák', 2, 2),
(3, 'Eva', 'Svobodová', 3, 3),
(4, 'Jiří', 'Dvořáček', 4, 4),
(5, 'Marie', 'Černá', 5, 5),
(6, 'Karel', 'Procházka', 6, 6),
(7, 'Jana', 'Kučerová', 7, 7),
(8, 'Pavel', 'Veselý', 8, 8),
(9, 'Lenka', 'Němcová', 9, 9),
(10, 'Martin', 'Král', 10, 10),
(11, 'Hana', 'Pokorná', 11, 11),
(12, 'Tomáš', 'Hájek', 12, 12),
(13, 'Michaela', 'Králová', 13, 13),
(14, 'Michal', 'Kovář', 14, 14),
(15, 'Lucie', 'Nováková', 15, 15),
(16, 'Václav', 'Zeman', 16, 16),
(17, 'Alena', 'Šimková', 17, 17),
(18, 'Roman', 'Urban', 18, 18),
(19, 'Jitka', 'Kolářová', 19, 19),
(20, 'Filip', 'Beneš', 20, 20),
(21, 'Zdeněk', 'Fiala', 21, 21),
(22, 'Petra', 'Malá', 22, 22),
(23, 'Vojtěch', 'Bartoš', 23, 23),
(24, 'Barbora', 'Vlčková', 24, 24),
(25, 'Jaroslav', 'Holý', 25, 25),
(26, 'Klára', 'Jelínková', 26, 26),
(27, 'Radek', 'Hruška', 27, 27),
(28, 'Martina', 'Sedláčková', 28, 28),
(29, 'Lukáš', 'Bláha', 29, 29),
(30, 'Anna', 'Růžičková', 30, 30);

-- --------------------------------------------------------

--
-- Struktura pro pohled `objednavky_produkty_view`
--
DROP TABLE IF EXISTS `objednavky_produkty_view`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `objednavky_produkty_view`  AS SELECT `o`.`ID` AS `objednavka_id`, `z`.`jmeno` AS `zakaznik_jmeno`, `p`.`nazev` AS `produkt_nazev`, `p`.`cena` AS `produkt_cena` FROM (((`objednavky` `o` join `zakaznici` `z` on(`o`.`ID_zakaznik` = `z`.`ID`)) join `obsah_objednavky` `oo` on(`o`.`ID` = `oo`.`ID_objednavka`)) join `produkty` `p` on(`oo`.`ID_produkt` = `p`.`ID`)) ;

--
-- Indexy pro exportované tabulky
--

--
-- Indexy pro tabulku `objednavky`
--
ALTER TABLE `objednavky`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `ID_vyrizujici` (`ID_vyrizujici`),
  ADD KEY `ID_zakaznik` (`ID_zakaznik`);

--
-- Indexy pro tabulku `obsah_objednavky`
--
ALTER TABLE `obsah_objednavky`
  ADD KEY `ID_objednavka` (`ID_objednavka`),
  ADD KEY `ID_produkt` (`ID_produkt`);

--
-- Indexy pro tabulku `oddeleni`
--
ALTER TABLE `oddeleni`
  ADD PRIMARY KEY (`ID`);

--
-- Indexy pro tabulku `pozice`
--
ALTER TABLE `pozice`
  ADD PRIMARY KEY (`ID`);

--
-- Indexy pro tabulku `produkty`
--
ALTER TABLE `produkty`
  ADD PRIMARY KEY (`ID`);

--
-- Indexy pro tabulku `sklad`
--
ALTER TABLE `sklad`
  ADD KEY `id_produkt` (`id_produkt`);

--
-- Indexy pro tabulku `uzivatele`
--
ALTER TABLE `uzivatele`
  ADD PRIMARY KEY (`id`);

--
-- Indexy pro tabulku `zakaznici`
--
ALTER TABLE `zakaznici`
  ADD PRIMARY KEY (`ID`);

--
-- Indexy pro tabulku `zamestnanci`
--
ALTER TABLE `zamestnanci`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `pozice` (`pozice`),
  ADD KEY `oddeleni` (`oddeleni`);

--
-- AUTO_INCREMENT pro tabulky
--

--
-- AUTO_INCREMENT pro tabulku `objednavky`
--
ALTER TABLE `objednavky`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pro tabulku `oddeleni`
--
ALTER TABLE `oddeleni`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pro tabulku `pozice`
--
ALTER TABLE `pozice`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pro tabulku `produkty`
--
ALTER TABLE `produkty`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT pro tabulku `uzivatele`
--
ALTER TABLE `uzivatele`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pro tabulku `zakaznici`
--
ALTER TABLE `zakaznici`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pro tabulku `zamestnanci`
--
ALTER TABLE `zamestnanci`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- Omezení pro exportované tabulky
--

--
-- Omezení pro tabulku `objednavky`
--
ALTER TABLE `objednavky`
  ADD CONSTRAINT `objednavky_ibfk_1` FOREIGN KEY (`ID_vyrizujici`) REFERENCES `zamestnanci` (`ID`),
  ADD CONSTRAINT `objednavky_ibfk_2` FOREIGN KEY (`ID_zakaznik`) REFERENCES `zakaznici` (`ID`);

--
-- Omezení pro tabulku `obsah_objednavky`
--
ALTER TABLE `obsah_objednavky`
  ADD CONSTRAINT `obsah_objednavky_ibfk_1` FOREIGN KEY (`ID_objednavka`) REFERENCES `objednavky` (`ID`),
  ADD CONSTRAINT `obsah_objednavky_ibfk_2` FOREIGN KEY (`ID_produkt`) REFERENCES `produkty` (`ID`);

--
-- Omezení pro tabulku `sklad`
--
ALTER TABLE `sklad`
  ADD CONSTRAINT `sklad_ibfk_1` FOREIGN KEY (`id_produkt`) REFERENCES `produkty` (`ID`);

--
-- Omezení pro tabulku `zamestnanci`
--
ALTER TABLE `zamestnanci`
  ADD CONSTRAINT `zamestnanci_ibfk_1` FOREIGN KEY (`pozice`) REFERENCES `pozice` (`ID`),
  ADD CONSTRAINT `zamestnanci_ibfk_2` FOREIGN KEY (`oddeleni`) REFERENCES `oddeleni` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

# Statnice-ULTIMATE
## OONV okruh 1 - Objektově orientované vzory
### Úloha 1-1 - Návrhové vzory ve správci kontaktů
Popis úlohy:
Vytvořte aplikaci pro správu kontaktů, která umožní uživatelům ukládat, prohlížet, upravovat
a mazat kontaktní informace. Při vývoji aplikace je třeba použít následující návrhové vzory:
1. Prototype: Pro efektivní vytváření a kopírování kontaktních záznamů.
2. Command: Pro zaznamenávání a možné vrácení změn v kontaktech.
3. Iterator: Pro navigaci skrze kolekci kontaktů.
Požadavky na odevzdání:
Řešitel odevzdá:
1. Funkční konzolová aplikace splňující níže uvedené funkční požadavky.
2. Zdrojový kód aplikace v repozitáři Github nebo Gitlab.
3. Stručná uživatelská příručka s návodem na použití aplikace.
Uživatelské požadavky:
1. Funkční požadavky:
○ Implementace přidávání nových kontaktů s detaily jako jméno, telefonní číslo,
e-mail, atd.
○ Implementace prohlížení, vyhledávání, úpravy a smazání existujících
kontaktů.
○ Možnost ukládání kontaktů do externího zdroje (stačí jednoduchý soubor).
○ Načítání kontaktů při spuštění aplikace.
2. Implementace návrhových vzorů:
○ Využití Prototype pro efektivní vytváření a kopírování kontaktů.
○ Implementace Command pro zaznamenávání a možnost vrácení změn
v kontaktech.
○ Použití Iterator pro navigaci skrze seznam kontaktů.
3. Uživatelské rozhraní:
○ Přehledné a intuitivní konzolové rozhraní pro jednoduchou navigaci
a ovládání aplikace.
4. Dokumentace a komentáře:
○ Stručná dokumentace kódu s popisem použitých návrhových vzorů.
○ Komentáře v kódu pro lepší pochopení struktury a logiky programu.
5. Testování:
○ Implementace jednotkových testů pro klíčové funkce aplikace.


### Úloha 1-2 - Návrhové vzory v aplikaci typu studijní systém
Popis úlohy:
Vytvořte konzolovou aplikaci v jazyce C#, která simuluje základní funkce studijního systému.
Aplikace by měla umožnit registraci studentů, správu kurzů, přihlašování na kurzy a zobrazení
studijních výsledků. Během vývoje aplikace je třeba použít následující tři návrhové vzory:
1. Factory Method: Implementujte pro vytváření různých typů uživatelských účtů
(student, učitel, správce) nebo kurzů.
2. Observer: Použijte pro informování studentů o změnách v kurzech nebo rozvrzích.
3. Strategy: Použijte pro změnu algoritmu výpočtu pro různé metody hodnocení
studentů (aritmetický průměr, vážený průměr apod.).
3
Požadavky na odevzdání:
Řešitel odevzdá:
1. Funkční konzolová aplikace splňující níže uvedené funkční požadavky.
2. Zdrojový kód aplikace v repozitáři Github nebo Gitlab.
3. Stručná uživatelská příručka s návodem na použití aplikace (stačí ve formě
README.md souboru).
Uživatelské požadavky:
1. Funkcionality:
○ Možnost registrace a přihlašování uživatelů (studentů, učitelů).
○ Správa kurzů včetně možnosti přidávání a upravování informací o kurzech.
○ Přihlašování studentů na kurzy a správa jejich studijního plánu.
○ Zobrazení studijních výsledků a průměrných známek.
2. Implementace Návrhových Vzorů:
○ Použití Observer vzoru pro sledování změn v kurzech a informování studentů.
○ Použití Factory Method vzoru pro vytváření různých typů účtů a kurzů.
○ Použití Strategy vzoru pro flexibilní způsoby hodnocení studentů.
3. Uživatelské Rozhraní:
○ Přehledné a intuitivní konzolové rozhraní pro jednoduchou navigaci
a ovládání aplikace.
4. Dokumentace a komentáře:
○ Stručná dokumentace kódu s popisem použitých návrhových vzorů.
○ Komentáře v kódu pro lepší pochopení struktury a logiky programu.
5. Testování:
○ Implementace jednotkových testů pro klíčové funkce aplikace.

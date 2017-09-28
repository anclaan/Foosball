alter table dbo.DruzynaZawodnicy
add constraint FK_Zawodnik1_Druzyna
foreign key (Zawodnik1) references Zawodnik(Id);

alter table dbo.DruzynaZawodnicy
add constraint FK_Zawodnik2_Druzyna
foreign key (Zawodnik2) references Zawodnik(Id);

alter table dbo.Druzyna
add constraint FK_DruzynaZ
foreign key (IdDruzyna) references DruzynaZawodnicy(Id);

alter table dbo.Mecz
add constraint FK_Mecz_Druzyna1
foreign key (IdDruzyna1) references Druzyna(Id);

alter table dbo.Mecz
add constraint FK_Mecz_Druzyna2
foreign key (IdDruzyna2) references Druzyna(Id);

namespace Quartalsarbeit_GR1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataToDB2 : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'853647f3-367b-483b-92cc-342feb2ea955', N'Administrator')

INSERT INTO [dbo].[AspNetUsers] ([Id], [Vorname], [Nachname], [TelPrivat], [TelGeschaeft], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'81b867d2-3b50-4082-ac1b-84b31d43b737', N'Franjo', N'Franjic', 0793311539, 0793311539, N'guest@login.ch', 0, N'AOXQ9wbGphpsTIiFXUjkPuNy7lyOGFUt+xM6RLTZAX+Qm4hloqKw5vhebtVcbogF9g==', N'e8a65ab5-55f7-4b7e-b409-d53b56be1b75', NULL, 0, 0, NULL, 1, 0, N'guest@login.ch')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Vorname], [Nachname], [TelPrivat], [TelGeschaeft], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f4a7a4b5-0986-4818-968e-9842e75d4d97', N'Admin', N'Admin', 0788888888, 0788888888, N'admin@login.ch', 0, N'AITjFWPv+TWgnd91gdUo/Q8ATVoT80t0Ycb6Los+vB0YfcoeeAr4D51Pkp56dGkomg==', N'7e815eae-1132-48d6-a2fc-e6cfc9ee7708', NULL, 0, 0, NULL, 1, 0, N'admin@login.ch')

INSERT INTO[dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES(N'f4a7a4b5-0986-4818-968e-9842e75d4d97', N'853647f3-367b-483b-92cc-342feb2ea955')

SET IDENTITY_INSERT [dbo].[Vereins] ON
INSERT INTO [dbo].[Vereins] ([ID], [Vereinsname], [Ort], [PLZ], [Strasse], [Vereinsverantwortlicher_Id]) VALUES (7, N'JUGI Wittenbach', N'Wittenbach', 9016, N'Hafenerstrasse 18', N'81b867d2-3b50-4082-ac1b-84b31d43b737')
SET IDENTITY_INSERT [dbo].[Vereins] OFF

SET IDENTITY_INSERT [dbo].[Athlets] ON
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (1, N'Lars', N'Muster', N'2005-02-02 00:00:00', 0, 165, 55, 7, NULL)
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (2, N'Roman', N'Meier', N'2004-03-20 00:00:00', 0, 170, 62, 7, NULL)
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (3, N'Marcel', N'Mayer', N'2004-10-22 00:00:00', 0, 177, 70, 7, NULL)
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (4, N'Aleks', N'Maier', N'2003-04-12 00:00:00', 0, 172, 77, 7, NULL)
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (5, N'Lisa', N'Müller', N'2006-08-14 00:00:00', 1, 162, 44, 7, NULL)
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (6, N'Julia', N'Müler', N'2007-09-17 00:00:00', 1, 164, 45, 7, NULL)
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (7, N'Lena', N'Mühler', N'2006-06-28 00:00:00', 1, 159, 40, 7, NULL)
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (8, N'Ivan', N'Mühller', N'2007-05-31 00:00:00', 0, 154, 48, 7, NULL)
INSERT INTO [dbo].[Athlets] ([ID], [Vorname], [Nachname], [Geburtstag], [Geschlecht], [Gewicht], [Groesse], [Verein_ID], [Anlass_ID]) VALUES (9, N'Samuel', N'Basler', N'2006-12-24 00:00:00', 0, 160, 55, 7, NULL)
SET IDENTITY_INSERT [dbo].[Athlets] OFF

SET IDENTITY_INSERT [dbo].[Disziplins] ON
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (1, N'Hochsprung', N'HS', N'resultat=15*hoehe+100', NULL)
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (2, N'Weitsprung', N'WS', N'resultat=13*weite+100', NULL)
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (3, N'200m Sprint', N'SP200', N'resultat=40*zeit+30', NULL)
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (4, N'100m Sprint', N'SP100', N'resultat=40*zeit+50', NULL)
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (5, N'80m Sprint', N'SP80', N'resultat=40*zeit+70', NULL)
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (6, N'Sackhüpfen', N'SH', N'resultat=15*zeit+100', NULL)
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (7, N'Ballwurf 200g', N'BW200', N'resultat=2*weite', NULL)
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (8, N'Ballwurf 80g', N'BW80', N'resultat=1.5*weite', NULL)
INSERT INTO [dbo].[Disziplins] ([ID], [Bezeichnung], [Abkuerzung], [Formel], [Config_ID]) VALUES (9, N'Kugelstossen', N'KS', N'resultat=12*weite+10', NULL)
SET IDENTITY_INSERT [dbo].[Disziplins] OFF

SET IDENTITY_INSERT [dbo].[Kategories] ON
INSERT INTO [dbo].[Kategories] ([ID], [Bezeichnung], [MinAlter], [MaxAlter], [Geschlecht]) VALUES (1, 'JKU18', 16, 17, 0)
INSERT INTO [dbo].[Kategories] ([ID], [Bezeichnung], [MinAlter], [MaxAlter], [Geschlecht]) VALUES (2, 'JKU16', 14, 15, 0)
INSERT INTO [dbo].[Kategories] ([ID], [Bezeichnung], [MinAlter], [MaxAlter], [Geschlecht]) VALUES (3, 'JKU14', 12, 13, 0)
INSERT INTO [dbo].[Kategories] ([ID], [Bezeichnung], [MinAlter], [MaxAlter], [Geschlecht]) VALUES (4, 'JMU18', 16, 17, 1)
INSERT INTO [dbo].[Kategories] ([ID], [Bezeichnung], [MinAlter], [MaxAlter], [Geschlecht]) VALUES (5, 'JMU16', 14, 15, 1)
INSERT INTO [dbo].[Kategories] ([ID], [Bezeichnung], [MinAlter], [MaxAlter], [Geschlecht]) VALUES (6, 'JMU14', 12, 13, 1)
SET IDENTITY_INSERT [dbo].[Kategories] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}

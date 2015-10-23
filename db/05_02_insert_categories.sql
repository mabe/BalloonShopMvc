USE BalloonShop
GO

TRUNCATE TABLE Category
GO

SET IDENTITY_INSERT Category ON
GO

INSERT INTO Category (CategoryID, DepartmentID, Name, Description )
VALUES (1, 1, 'Love & Romance', 'Here''s our collection of balloons with romantic messages.'),
(2, 1, 'Birthdays', 'Tell someone "Happy Birthday" with one of these wonderful balloons!'),
(3, 1, 'Weddings', 'Going to a wedding? Here''s a collection of balloons for that special event!'),
(4, 2, 'Message Balloons', 'Why write on paper, when you can deliver your message on a balloon?'),
(5, 2, 'Cartoons', 'Buy a balloon with your child''s favorite cartoon character!'),
(6, 2, 'Miscellaneous', 'Various baloons that your kid will most certainly love!');

GO

SET IDENTITY_INSERT Category OFF
GO

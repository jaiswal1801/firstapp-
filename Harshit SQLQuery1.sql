CREATE PROC UserAddOrEditt
@UserID int,
@FirstName varchar(50),
@LastName varchar(50),
@Contact varchar(50),
@Gender varchar(10),
@Address varchar(250),
@Username varchar(50),
@Password varchar(50)
AS
   IF @UserID = 0
   BEGIN
       INSERT INTO UserRegisteration (FirstName,LastName,Contact,Gender,Address,Username,Password)
       VALUES (@FirstName,@LastName,@Contact,@Gender,@Address,@Username,@Password)
   END
   ELSE
   BEGIN
       UPDATE UserRegisteration
       SET
          FirstName = @FirstName,
          LastName = @LastName,
          Contact = @Contact,
          Gender = @Gender,
          Address = @Address,
          Username = @Username,
          Password = @Password
       WHERE UserID = @UserID
   END
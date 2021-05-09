CREATE PROC UserViewByyID
@UserID int
AS
   SELECT *
   FROM UserRegisteration
   WHERE UserID = @UserID
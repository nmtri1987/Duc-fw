IF object_id('[dbo].[DNHUsers_GetAll]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHUsers_GetAll]
                            GO
CREATE PROCEDURE [dbo].[DNHUsers_GetAll]
		@CompanyID int 
                        AS
                    SELECT * FROM DNHUsers where CompanyID=@CompanyID;
                    GO

IF object_id('[dbo].[DNHUsers_GetAll_byUser]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHUsers_GetAll_byUser]
                            GO
CREATE PROCEDURE [dbo].[DNHUsers_GetAll_byUser]
@CreatedUser  nvarchar(25),
@CompanyID int 
                        AS
                    SELECT * FROM DNHUsers where CreatedUser=@CreatedUser and CompanyID=@CompanyID;
                    GO

IF object_id('[dbo].[DNHUsers_GetByID]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHUsers_GetByID]
                            GO
CREATE PROCEDURE [dbo].[DNHUsers_GetByID]
                @Id nvarchar(128) ,
				@CompanyID int 
                        AS
                    SELECT * FROM DNHUsers where Id=@Id and CompanyID=@CompanyID;
                    GO
IF object_id('[dbo].[DNHUsers_Delete]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHUsers_Delete]
                            GO
CREATE PROCEDURE [dbo].[DNHUsers_Delete]
@Id nvarchar(128) ,@CompanyID int
 AS
                    Delete  FROM DNHUsers where Id=@Id
 and CompanyID=@CompanyID
GO

IF object_id('[dbo].[DNHUsers_Search]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHUsers_Search]
                            GO
CREATE procedure [dbo].[DNHUsers_Search]
	@Keyword nvarchar(100) = null,
	@Columns nvarchar(500) = null,
    @OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@CompanyID int,
	@Condition nvarchar(500)=null

as

    declare @PageLowerBound int
	declare @PageUpperBound int
	set @PageLowerBound = (@Page - 1)*@PageSize+1
	set @PageUpperBound = @Page*@PageSize

	declare @sql nvarchar(2000)
	declare @para nvarchar(500)

	set @para = N'@PageLowerBound int,@PageUpperBound int,@Keyword nvarchar(100),@Columns nvarchar(500),@CompanyID int'

	set @sql = N';with tbl as
				(
					select row_number() over(order by tbl.['+ @OrderBy + N'] '+@OrderDirection+') as RowNumber,tbl.*,count(1) over() as TotalRecord
					from [dbo].[DNHUsers] tbl with(nolock)
					where (tbl.Companyid =@CompanyID) '
	if(@Keyword is not null and @Columns is not null)
		begin
			declare @str_keyword nvarchar(500)=' and(', @Col nvarchar(20)
			set @Keyword='%'+@Keyword+'%'
			--set @sql += ' and tbl.UserName like @Keyword'
			if @Columns is not null and @Columns <> ''
				begin
					declare CUR CURSOR
						LOCAL
						FORWARD_ONLY
						STATIC
						READ_ONLY
						TYPE_WARNING
					FOR
						select [Value] from dbo.FNSplitString(@Columns, ',')
					OPEN CUR
					FETCH NEXT FROM CUR INTO @Col
					WHILE @@FETCH_STATUS = 0
						begin							
							set @str_keyword += 'tbl.' + @Col + ' like @Keyword or '
							FETCH NEXT FROM CUR INTO @Col
						end
					close CUR
					deallocate CUR
					select @str_keyword = left(@str_keyword, LEN(@str_keyword) - 3)+')'
				end
			set @sql += @str_keyword
		end
		if(@Condition is not null and @Condition<>'')
		begin
			set @sql +=' and ('+ @Condition+') '
		end
    set @sql += ')
				select tbl.*
				from tbl
				where tbl.RowNumber between @PageLowerBound and @PageUpperBound'

	execute sp_executesql @sql,@para,@PageLowerBound=@PageLowerBound,@PageUpperBound=@PageUpperBound, @Keyword = @Keyword,@Columns=@Columns,@CompanyID=@CompanyID
    
GO


IF object_id('[dbo].[DNHUsers_Add]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHUsers_Add]
                            GO
CREATE PROCEDURE [dbo].[DNHUsers_Add]
                @CompanyID int,
@Id nvarchar(128),
@Email nvarchar(256),
@EmailConfirmed bit,
@PasswordHash nvarchar,
@SecurityStamp nvarchar,
@PhoneNumber nvarchar,
@PhoneNumberConfirmed bit,
@TwoFactorEnabled bit,
@LockoutEndDateUtc datetime,
@LockoutEnabled bit,
@AccessFailedCount int,
@UserName nvarchar(256),
@CreatedUser nvarchar(256),
@CreatedDate datetime,
@IsAdmin bit,
@Application nvarchar(32)

                        AS 
SET @Id = ISNULL((select top 1  Id  from DNHUsers where CompanyID = @CompanyID order by Id desc),0) +1 
Insert Into DNHUsers(CompanyID,Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,CreatedUser,CreatedDate,IsAdmin,Application)
Values(@CompanyID,@Id,@Email,@EmailConfirmed,@PasswordHash,@SecurityStamp,@PhoneNumber,@PhoneNumberConfirmed,@TwoFactorEnabled,@LockoutEndDateUtc,@LockoutEnabled,@AccessFailedCount,@UserName,@CreatedUser,@CreatedDate,@IsAdmin,@Application)
SELECT @Id
GO

IF object_id('[dbo].[DNHUsers_Update]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHUsers_Update]
                            GO
CREATE PROCEDURE [dbo].[DNHUsers_Update]
@CompanyID int,
@Id nvarchar(128),
@Email nvarchar(256),
@EmailConfirmed bit,
@PasswordHash nvarchar,
@SecurityStamp nvarchar,
@PhoneNumber nvarchar,
@PhoneNumberConfirmed bit,
@TwoFactorEnabled bit,
@LockoutEndDateUtc datetime,
@LockoutEnabled bit,
@AccessFailedCount int,
@UserName nvarchar(256),
@CreatedUser nvarchar(256),
@CreatedDate datetime,
@IsAdmin bit,
@Application nvarchar(32)

AS
UPDATE DNHUsers
SET
CompanyID= @CompanyID,

Email= @Email,
EmailConfirmed= @EmailConfirmed,
PasswordHash= @PasswordHash,
SecurityStamp= @SecurityStamp,
PhoneNumber= @PhoneNumber,
PhoneNumberConfirmed= @PhoneNumberConfirmed,
TwoFactorEnabled= @TwoFactorEnabled,
LockoutEndDateUtc= @LockoutEndDateUtc,
LockoutEnabled= @LockoutEnabled,
AccessFailedCount= @AccessFailedCount,
UserName= @UserName,
CreatedUser= @CreatedUser,
CreatedDate= @CreatedDate,
IsAdmin= @IsAdmin,
Application= @Application

Where Id= @Id
 and CompanyID = @CompanyID
GO


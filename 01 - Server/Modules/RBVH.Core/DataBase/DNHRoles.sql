IF object_id('[dbo].[DNHRoles_GetAll]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHRoles_GetAll]
                            GO
CREATE PROCEDURE [dbo].[DNHRoles_GetAll]
		@CompanyID int 
                        AS
                    SELECT * FROM DNHRoles where CompanyID=@CompanyID;
                    GO

IF object_id('[dbo].[DNHRoles_GetAll_byUser]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHRoles_GetAll_byUser]
                            GO
CREATE PROCEDURE [dbo].[DNHRoles_GetAll_byUser]
@CreatedUser  nvarchar(25),
@CompanyID int 
                        AS
                    SELECT * FROM DNHRoles where CreatedUser=@CreatedUser and CompanyID=@CompanyID;
                    GO

IF object_id('[dbo].[DNHRoles_GetByID]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHRoles_GetByID]
                            GO
CREATE PROCEDURE [dbo].[DNHRoles_GetByID]
                @Rolename nvarchar(64) ,
				@CompanyID int 
                        AS
                    SELECT * FROM DNHRoles where Rolename=@Rolename and CompanyID=@CompanyID;
                    GO
IF object_id('[dbo].[DNHRoles_Delete]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHRoles_Delete]
                            GO
CREATE PROCEDURE [dbo].[DNHRoles_Delete]
@Rolename nvarchar(64) ,@CompanyID int
 AS
                    Delete  FROM DNHRoles where Rolename=@Rolename
 and CompanyID=@CompanyID
GO

IF object_id('[dbo].[DNHRoles_Search]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHRoles_Search]
                            GO
CREATE procedure [dbo].[DNHRoles_Search]
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
					from [dbo].[DNHRoles] tbl with(nolock)
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


IF object_id('[dbo].[DNHRoles_Add]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHRoles_Add]
                            GO
CREATE PROCEDURE [dbo].[DNHRoles_Add]
                @CompanyID int,
@Rolename nvarchar(64),
@ApplicationName varchar(32),
@Descr nvarchar(255),
@isGuest bit,
@Createduser nvarchar(50),
@CreatedDate datetime

                        AS 
Insert Into DNHRoles(CompanyID,Rolename,ApplicationName,Descr,isGuest,Createduser,CreatedDate)
Values(@CompanyID,@Rolename,@ApplicationName,@Descr,@isGuest,@Createduser,@CreatedDate)
SELECT @Rolename
GO

IF object_id('[dbo].[DNHRoles_Update]') IS NOT NULL
                                DROP PROCEDURE [dbo].[DNHRoles_Update]
                            GO
CREATE PROCEDURE [dbo].[DNHRoles_Update]
@CompanyID int,
@Rolename nvarchar(64),
@ApplicationName varchar(32),
@Descr nvarchar(255),
@isGuest bit,
@Createduser nvarchar(50),
@CreatedDate datetime

AS
UPDATE DNHRoles
SET
CompanyID= @CompanyID,

ApplicationName= @ApplicationName,
Descr= @Descr,
isGuest= @isGuest,
Createduser= @Createduser,
CreatedDate= @CreatedDate

Where Rolename= @Rolename
 and CompanyID = @CompanyID
GO


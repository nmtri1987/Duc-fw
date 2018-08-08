IF object_id('[dbo].[T_COM_Master_EmployeeEntityAccess_GetAll]') IS NOT NULL
                                DROP PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_GetAll]
                            GO
CREATE PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_GetAll]

                        AS
                    SELECT * FROM T_COM_Master_EmployeeEntityAccess;
                    GO

IF object_id('[dbo].[T_COM_Master_EmployeeEntityAccess_GetAll_byUser]') IS NOT NULL
                                DROP PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_GetAll_byUser]
                            GO
CREATE PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_GetAll_byUser]
@CreatedUser  nvarchar(25)
                        AS
                    SELECT * FROM T_COM_Master_EmployeeEntityAccess where CreatedUser=@CreatedUser;
                    GO

IF object_id('[dbo].[T_COM_Master_EmployeeEntityAccess_GetByID]') IS NOT NULL
                                DROP PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_GetByID]
                            GO
CREATE PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_GetByID]
                @EmployeeCode int 
                        AS
                    SELECT * FROM T_COM_Master_EmployeeEntityAccess where EmployeeCode=@EmployeeCode;
                    GO
IF object_id('[dbo].[T_COM_Master_EmployeeEntityAccess_Delete]') IS NOT NULL
                                DROP PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_Delete]
                            GO
CREATE PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_Delete]
@EmployeeCode int AS
                    Delete  FROM T_COM_Master_EmployeeEntityAccess where EmployeeCode=@EmployeeCode
GO

IF object_id('[dbo].[T_COM_Master_EmployeeEntityAccess_Search]') IS NOT NULL
                                DROP PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_Search]
                            GO
CREATE procedure [dbo].[T_COM_Master_EmployeeEntityAccess_Search]
	@Keyword nvarchar(100) = null,
	@Columns nvarchar(500) = null,
    @OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@Condition nvarchar(500)=null

as

    declare @PageLowerBound int
	declare @PageUpperBound int
	set @PageLowerBound = (@Page - 1)*@PageSize+1
	set @PageUpperBound = @Page*@PageSize

	declare @sql nvarchar(2000)
	declare @para nvarchar(500)

	set @para = N'@PageLowerBound int,@PageUpperBound int,@Keyword nvarchar(100),@Columns nvarchar(500)'

	set @sql = N';with tbl as
				(
					select row_number() over(order by tbl.['+ @OrderBy + N'] '+@OrderDirection+') as RowNumber,tbl.*,count(1) over() as TotalRecord
					from [dbo].[T_COM_Master_EmployeeEntityAccess] tbl with(nolock)
					where (tbl.EmployeeCode is not null) '
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

	execute sp_executesql @sql,@para,@PageLowerBound=@PageLowerBound,@PageUpperBound=@PageUpperBound, @Keyword = @Keyword ,@Columns=@Columns

GO
    


IF object_id('[dbo].[T_COM_Master_EmployeeEntityAccess_Add]') IS NOT NULL
                                DROP PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_Add]
                            GO
CREATE PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_Add]
                @EmployeeCode int,
@EntityID int,
@DomainID nvarchar(25),
@isActive bit,
@CreatedDate datetime,
@CreatedBy int

                        AS 
Insert Into T_COM_Master_EmployeeEntityAccess(EmployeeCode,EntityID,DomainID,isActive,CreatedDate,CreatedBy)
Values(@EmployeeCode,@EntityID,@DomainID,@isActive,@CreatedDate,@CreatedBy)
SELECT @EmployeeCode
GO

IF object_id('[dbo].[T_COM_Master_EmployeeEntityAccess_Update]') IS NOT NULL
                                DROP PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_Update]
                            GO
CREATE PROCEDURE [dbo].[T_COM_Master_EmployeeEntityAccess_Update]
@EmployeeCode int,
@EntityID int,
@DomainID nvarchar(25),
@isActive bit,
@CreatedDate datetime,
@CreatedBy int

AS
UPDATE T_COM_Master_EmployeeEntityAccess
SET
EmployeeCode= @EmployeeCode,

DomainID= @DomainID,
isActive= @isActive,
CreatedDate= @CreatedDate,
CreatedBy= @CreatedBy

Where EntityID= @EntityID
GO


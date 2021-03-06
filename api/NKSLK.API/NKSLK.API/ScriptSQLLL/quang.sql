USE [Nhom6_NKSLK]
GO
/****** Object:  StoredProcedure [dbo].[nkslk_by_month_week]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	create proc [dbo].[nkslk_by_month_week]
	(
		@month int,
		@week int,
		@manhanvien int
	)
	as
	begin 
		select n.ma_nkslk,cn.ma_congnhan, cn.hoten, ngaybatdau,n.thoigian_batdau, n.thoigian_batdau
		from NKSLK n,NKSLK_CONGNHAN nc, CONGNHAN cn
		where (@month IS NULL OR month(ngaybatdau) = @month) 
			  and (@week IS NULL OR DATEPART(week, ngaybatdau) = @week)
			  and n.ma_nkslk = nc.ma_nkslk 
			  and nc.ma_congnhan = cn.ma_congnhan
			  and (@manhanvien IS NULL OR cn.ma_congnhan = @manhanvien) 
		group by n.ma_nkslk,cn.ma_congnhan, cn.hoten, ngaybatdau,n.thoigian_batdau, n.thoigian_batdau

	end
GO
/****** Object:  StoredProcedure [dbo].[Proc_DeleteTasks]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Proc_DeleteTasks](@id int)
as begin
Delete CONGVIEC where ma_congviec = @id;
end;
GO
/****** Object:  StoredProcedure [dbo].[Proc_DeleteUnitTasks]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_DeleteUnitTasks](@id int)
AS
BEGIN
delete DONVIKHOAN where ma_donvikhoan = @id
END;
GO
/****** Object:  StoredProcedure [dbo].[proc_get_detail_salary_employee_by_month]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_get_detail_salary_employee_by_month](@p_month int,@p_year int,@p_ma_congnhan int)
	as
	begin
		DECLARE @month INT, @year INT
		IF(@p_month is null)
			SET @month=MONTH(GETDATE());
		ELSE 
			SET @month=@p_month;

		IF(@p_year is null)
			SET @year=YEAR(GETDATE());
		ELSE 
			SET @year=@p_year;


		IF(@p_ma_congnhan is null)
				select n.ma_congnhan,cn.hoten,
		   n.thoigian_batdau,
		   n.thoigian_ketthuc,
		   n.sogio_lamviec,
		   n.ma_nkslk,
		   vnt.tonggiolamviec,
		   vnt2.tongsotiencongviec,
		   vnt2.tongsotiencongviec*(n.sogio_lamviec/vnt.tonggiolamviec) 
	from	NKSLK_CONGNHAN n
			inner join view_nkslk_tongsogiolamviec  vnt on n.ma_nkslk = vnt.ma_nkslk
			inner join view_nkslk_tongsotiencongviec  vnt2 on n.ma_nkslk = vnt2.ma_nkslk
			inner join CONGNHAN cn on n.ma_congnhan=cn.ma_congnhan
	where YEAR(n.thoigian_batdau) = @year 
		  and MONTH(n.thoigian_batdau) = @month
	order by n.thoigian_batdau;
		ELSE 
		select n.ma_congnhan,cn.hoten,
		   n.thoigian_batdau,
		   n.thoigian_ketthuc,
		   n.sogio_lamviec,
		   n.ma_nkslk,
		   vnt.tonggiolamviec,
		   vnt2.tongsotiencongviec,
		   vnt2.tongsotiencongviec*(n.sogio_lamviec/vnt.tonggiolamviec) 
	from	NKSLK_CONGNHAN n
			inner join view_nkslk_tongsogiolamviec  vnt on n.ma_nkslk = vnt.ma_nkslk
			inner join view_nkslk_tongsotiencongviec  vnt2 on n.ma_nkslk = vnt2.ma_nkslk
			inner join CONGNHAN cn on n.ma_congnhan=cn.ma_congnhan
	where n.ma_congnhan=@p_ma_congnhan and  YEAR(n.thoigian_batdau) = @year 
		  and MONTH(n.thoigian_batdau) = @month
	order by n.thoigian_batdau;
	end
GO
/****** Object:  StoredProcedure [dbo].[proc_get_detail_salary_employee_by_week]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_get_detail_salary_employee_by_week](@p_week int,@p_year int,@p_ma_congnhan int)
	as
	begin
		DECLARE @week INT, @year INT
		IF(@p_week is null)
			SET @week=DATEPART(week, GETDATE())
		ELSE 
			SET @week=@p_week;

		IF(@p_year is null)
			SET @year=YEAR(GETDATE());
		ELSE 
			SET @year=@p_year;


		IF(@p_ma_congnhan is null)
				select n.ma_congnhan,cn.hoten,
		   n.thoigian_batdau,
		   n.thoigian_ketthuc,
		   n.sogio_lamviec,
		   n.ma_nkslk,
		   vnt.tonggiolamviec,
		   vnt2.tongsotiencongviec,
		   vnt2.tongsotiencongviec*(n.sogio_lamviec/vnt.tonggiolamviec) 
	from	NKSLK_CONGNHAN n
			inner join view_nkslk_tongsogiolamviec  vnt on n.ma_nkslk = vnt.ma_nkslk
			inner join view_nkslk_tongsotiencongviec  vnt2 on n.ma_nkslk = vnt2.ma_nkslk
			inner join CONGNHAN cn on n.ma_congnhan=cn.ma_congnhan
	where YEAR(n.thoigian_batdau) = @year 
		  and DATEPART(week, n.thoigian_batdau) = @week
	order by n.thoigian_batdau;
		ELSE 
		select n.ma_congnhan,cn.hoten,
		   n.thoigian_batdau,
		   n.thoigian_ketthuc,
		   n.sogio_lamviec,
		   n.ma_nkslk,
		   vnt.tonggiolamviec,
		   vnt2.tongsotiencongviec,
		   vnt2.tongsotiencongviec*(n.sogio_lamviec/vnt.tonggiolamviec) 
	from	NKSLK_CONGNHAN n
			inner join view_nkslk_tongsogiolamviec  vnt on n.ma_nkslk = vnt.ma_nkslk
			inner join view_nkslk_tongsotiencongviec  vnt2 on n.ma_nkslk = vnt2.ma_nkslk
			inner join CONGNHAN cn on n.ma_congnhan=cn.ma_congnhan
	where n.ma_congnhan=@p_ma_congnhan and  YEAR(n.thoigian_batdau) = @year 
		  and DATEPART(week, n.thoigian_batdau) = @week
	order by n.thoigian_batdau;
	end
GO
/****** Object:  StoredProcedure [dbo].[proc_get_nkslk_by_month]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_get_nkslk_by_month](@p_month int,@p_year int)
	as
	begin
		DECLARE @month INT, @year INT
		IF(@p_month is null)
			SET @month=MONTH(GETDATE());
		ELSE 
			SET @month=@p_month;

		IF(@p_year is null)
			SET @year=YEAR(GETDATE());
		ELSE 
			SET @year=@p_year;


		select 
			nk.ma_nkslk as N'mã NKSLK',
			nk.ngaybatdau as N'Ngày bắt đầu',
			nk.thoigian_batdau as N'Thời gian bắt đầu',
			nk.thoigian_ketthuc as N'Thời gian kết thúc'
		from NKSLK nk
		where YEAR(nk.ngaybatdau) = @year and MONTH(nk.ngaybatdau) = @month
	end
GO
/****** Object:  StoredProcedure [dbo].[proc_get_nkslk_by_week]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_get_nkslk_by_week](@p_week int,@p_year int)
	as
	begin
		DECLARE @week INT, @year INT
		IF(@p_week is null)
			SET @week=DATEPART(week, GETDATE())
		ELSE 
			SET @week=@p_week;

		IF(@p_year is null)
			SET @year=YEAR(GETDATE());
		ELSE 
			SET @year=@p_year;
		select 
			nk.ma_nkslk as N'Mã NKSLK',
			nk.ngaybatdau as N'Ngày bắt đầu',
			nk.thoigian_batdau as N'Thời gian bắt đầu',
			nk.thoigian_ketthuc as N'Thời gian kết thúc'
		from NKSLK nk
		where YEAR(nk.ngaybatdau) = @year and DATEPART(week, nk.ngaybatdau) = @week
	end
GO
/****** Object:  StoredProcedure [dbo].[proc_get_sum_salary_employee_by_month]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_get_sum_salary_employee_by_month]
	(
		@p_month int,
		@p_year int,
		@p_ma_congnhan int
	)
	as
	begin
		DECLARE @month INT, @year INT
		IF(@p_month is null)
			SET @month=MONTH(GETDATE());
		ELSE 
			SET @month=@p_month;

		IF(@p_year is null)
			SET @year=YEAR(GETDATE());
		ELSE 
			SET @year=@p_year;


		IF(@p_ma_congnhan is null)
			select n.ma_congnhan,
		   cn.hoten,
		   sum(vnt2.tongsotiencongviec*(n.sogio_lamviec/vnt.tonggiolamviec)) as tongtien 
	from	NKSLK_CONGNHAN n
			inner join view_nkslk_tongsogiolamviec  vnt on n.ma_nkslk = vnt.ma_nkslk
			inner join view_nkslk_tongsotiencongviec  vnt2 on n.ma_nkslk = vnt2.ma_nkslk
			inner join CONGNHAN cn on n.ma_congnhan=cn.ma_congnhan
	where YEAR(n.thoigian_batdau) = @year 
		  and MONTH(n.thoigian_batdau) = @month 
	group by n.ma_congnhan,cn.hoten
	order by cn.hoten;
		ELSE 
		select n.ma_congnhan,
		   cn.hoten,
		   sum(vnt2.tongsotiencongviec*(n.sogio_lamviec/vnt.tonggiolamviec)) as tongtien 
	from	NKSLK_CONGNHAN n
			inner join view_nkslk_tongsogiolamviec  vnt on n.ma_nkslk = vnt.ma_nkslk
			inner join view_nkslk_tongsotiencongviec  vnt2 on n.ma_nkslk = vnt2.ma_nkslk
			inner join CONGNHAN cn on n.ma_congnhan=cn.ma_congnhan
	where YEAR(n.thoigian_batdau) = @year 
		  and MONTH(n.thoigian_batdau) = @month 
		  and n.ma_congnhan = @p_ma_congnhan
	group by n.ma_congnhan,cn.hoten
	order by cn.hoten;
			
	end
GO
/****** Object:  StoredProcedure [dbo].[proc_get_sum_salary_employee_by_week]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_get_sum_salary_employee_by_week]
	(
		@p_week int,
		@p_year int,
		@p_ma_congnhan int
	)
	as
	begin
		DECLARE @week INT, @year INT
		IF(@p_week is null)
			SET @week=DATEPART(week, GETDATE())
		ELSE 
			SET @week=@p_week;

		IF(@p_year is null)
			SET @year=YEAR(GETDATE());
		ELSE 
			SET @year=@p_year;

		IF(@p_ma_congnhan is null)
			select n.ma_congnhan as N'Mã công nhân',
		   cn.hoten as N'Họ tên công nhân',
		   sum(vnt2.tongsotiencongviec*(n.sogio_lamviec/vnt.tonggiolamviec)) as N'Tổng lương' 
	from	NKSLK_CONGNHAN n
			inner join view_nkslk_tongsogiolamviec  vnt on n.ma_nkslk = vnt.ma_nkslk
			inner join view_nkslk_tongsotiencongviec  vnt2 on n.ma_nkslk = vnt2.ma_nkslk
			inner join CONGNHAN cn on n.ma_congnhan=cn.ma_congnhan
	where YEAR(n.thoigian_batdau) = @year and
		  DATEPART(week, n.thoigian_batdau) = @week
	group by n.ma_congnhan,cn.hoten
	order by cn.hoten;
		ELSE 
		select n.ma_congnhan as N'Mã công nhân',
		   cn.hoten as N'Họ tên công nhân',
		   sum(vnt2.tongsotiencongviec*(n.sogio_lamviec/vnt.tonggiolamviec)) as N'Tổng lương' 
	from	NKSLK_CONGNHAN n
			inner join view_nkslk_tongsogiolamviec  vnt on n.ma_nkslk = vnt.ma_nkslk
			inner join view_nkslk_tongsotiencongviec  vnt2 on n.ma_nkslk = vnt2.ma_nkslk
			inner join CONGNHAN cn on n.ma_congnhan=cn.ma_congnhan
	where YEAR(n.thoigian_batdau) = @year and
		  DATEPART(week, n.thoigian_batdau) = @week and n.ma_congnhan = @p_ma_congnhan
	group by n.ma_congnhan,cn.hoten
	order by cn.hoten;
			

		
	end
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAllJob]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_GetAllJob]
AS
BEGIN
select * from CONGVIEC
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAllTasks]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_GetAllTasks]
AS
BEGIN
select cv.ma_congviec tasks_id,
cv.tencongviec tasks_name,
cv.ma_donvikhoan  unittasks_id,
dv.ma_tendonvikhoan unittasks_name,
cv.dinhmuckhoan litmit_unit,
cv.hesokhoan limit_rate,
cv.dinhmuclaodong litmit_work,
cv.dongia price
from CONGVIEC cv inner join DONVIKHOAN dv on cv.ma_donvikhoan = dv.ma_donvikhoan
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAllUnitTasks]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_GetAllUnitTasks]
AS
BEGIN
select ma_donvikhoan as unittasks_id , ma_tendonvikhoan as unittasks_name from DONVIKHOAN order by ma_donvikhoan
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetJobByMutilSalary]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_GetJobByMutilSalary](@salaryMin decimal,@salaryMax decimal)
AS
BEGIN
select * from CONGVIEC where dongia  between @salaryMin and @salaryMax order by tencongviec
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetJobBySalary]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_GetJobBySalary](@salary decimal)
AS
BEGIN
select * from CONGVIEC where dongia = @salary order by tencongviec
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMaxJobByNKSLK]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_GetMaxJobByNKSLK] 
AS
BEGIN
select nkvc.ma_congviec tasks_id,
cv.tencongviec tasks_name,
dv.ma_tendonvikhoan unittasks_name,
cv.dinhmuckhoan litmit_unit,
cv.hesokhoan limit_rate,
cv.dinhmuclaodong litmit_work,
cv.dongia price,
count(nkvc.ma_nkslk) quantityNKSLK
from NKSLK_CONGVIEC nkvc
inner join CONGVIEC cv on cv.ma_congviec = nkvc.ma_congviec
inner join DONVIKHOAN dv on cv.ma_donvikhoan = dv.ma_donvikhoan
group by nkvc.ma_congviec ,cv.tencongviec,dv.ma_tendonvikhoan,cv.dinhmuckhoan,
cv.hesokhoan,
cv.dinhmuclaodong,
cv.dongia
having count(nkvc.ma_nkslk) = (select max(t.soluong) from
(select count(ma_nkslk) soluong from NKSLK_CONGVIEC
group by ma_congviec )as t)
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMinJobByNKSLK]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure [dbo].[Proc_GetMinJobByNKSLK] 
AS
BEGIN
select nkvc.ma_congviec tasks_id,
cv.tencongviec tasks_name,
dv.ma_tendonvikhoan unittasks_name,
cv.dinhmuckhoan litmit_unit,
cv.hesokhoan limit_rate,
cv.dinhmuclaodong litmit_work,
cv.dongia price,
count(nkvc.ma_nkslk) quantityNKSLK
from NKSLK_CONGVIEC nkvc
inner join CONGVIEC cv on cv.ma_congviec = nkvc.ma_congviec
inner join DONVIKHOAN dv on cv.ma_donvikhoan = dv.ma_donvikhoan
group by nkvc.ma_congviec ,cv.tencongviec,dv.ma_tendonvikhoan,cv.dinhmuckhoan,
cv.hesokhoan,
cv.dinhmuclaodong,
cv.dongia
having count(nkvc.ma_nkslk) = (select min(t.soluong) from
(select count(ma_nkslk) soluong from NKSLK_CONGVIEC
group by ma_congviec )as t)
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTaskForDelete]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Proc_GetTaskForDelete](@id int)
as begin
select * from NKSLK_CONGVIEC where ma_congviec = @id
end;
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTasksBiggerAVG]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Proc_GetTasksBiggerAVG]
as
begin
select cv.ma_congviec tasks_id,
cv.tencongviec tasks_name,
cv.ma_donvikhoan  unittasks_id,
dv.ma_tendonvikhoan unittasks_name,
cv.dinhmuckhoan litmit_unit,
cv.hesokhoan limit_rate,
cv.dinhmuclaodong litmit_work,
cv.dongia price
from CONGVIEC cv inner join DONVIKHOAN dv on cv.ma_donvikhoan = dv.ma_donvikhoan
	where dongia > (select AVG(dongia) from congviec ) 
end
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTasksLessAVG]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Proc_GetTasksLessAVG]
as
begin
	select cv.ma_congviec tasks_id,
cv.tencongviec tasks_name,
cv.ma_donvikhoan  unittasks_id,
dv.ma_tendonvikhoan unittasks_name,
cv.dinhmuckhoan litmit_unit,
cv.hesokhoan limit_rate,
cv.dinhmuclaodong litmit_work,
cv.dongia price
from CONGVIEC cv inner join DONVIKHOAN dv on cv.ma_donvikhoan = dv.ma_donvikhoan
	where dongia < (select AVG(dongia) from congviec ) 
	end
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTasksMaxPrice]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Proc_GetTasksMaxPrice]
as
begin
	select cv.ma_congviec tasks_id,
cv.tencongviec tasks_name,
cv.ma_donvikhoan  unittasks_id,
dv.ma_tendonvikhoan unittasks_name,
cv.dinhmuckhoan litmit_unit,
cv.hesokhoan limit_rate,
cv.dinhmuclaodong litmit_work,
cv.dongia price
from CONGVIEC cv inner join DONVIKHOAN dv on cv.ma_donvikhoan = dv.ma_donvikhoan
where cv.dongia = (select MAX(dongia) from congviec )
end
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTasksMinPrice]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Proc_GetTasksMinPrice]
as
begin
	select cv.ma_congviec tasks_id,
cv.tencongviec tasks_name,
cv.ma_donvikhoan  unittasks_id,
dv.ma_tendonvikhoan unittasks_name,
cv.dinhmuckhoan litmit_unit,
cv.hesokhoan limit_rate,
cv.dinhmuclaodong litmit_work,
cv.dongia price
from CONGVIEC cv inner join DONVIKHOAN dv on cv.ma_donvikhoan = dv.ma_donvikhoan
	where cv.dongia = (select MIN(dongia) from congviec )
end
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTasksWithUnitId]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_GetTasksWithUnitId](@id int)
AS
BEGIN
select * from CONGVIEC where ma_donvikhoan = @id
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertTasks]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure [dbo].[Proc_InsertTasks](@name nvarchar(500),@litmit_unit int,@unittasks_id int ,@limit_rate float,@litmit_work int)
AS
BEGIN
INSERT INTO [dbo].[CONGVIEC] ([tencongviec],[dinhmuckhoan],[ma_donvikhoan],[hesokhoan],[dinhmuclaodong],[dongia])
VALUES (@name,@litmit_unit,@unittasks_id,@limit_rate,@litmit_work ,null);
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertUnitTasks]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_InsertUnitTasks](@name nvarchar(500))
AS
BEGIN
insert into DONVIKHOAN(ma_tendonvikhoan) values(@name)
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_UpdateTasks]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure [dbo].[Proc_UpdateTasks](@id int,@name nvarchar(500),@litmit_unit int,@unittasks_id int ,@limit_rate float,@litmit_work int)
AS
BEGIN
update CONGVIEC set tencongviec=@name ,dinhmuckhoan=@litmit_unit, ma_donvikhoan=@unittasks_id, hesokhoan=@limit_rate, dinhmuclaodong=@litmit_work, dongia =null
where ma_congviec = @id;
END;
GO
/****** Object:  StoredProcedure [dbo].[Proc_UpdateUnitTasks]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_UpdateUnitTasks](@id int,@name nvarchar(500))
AS
BEGIN
update DONVIKHOAN set ma_tendonvikhoan = @name where ma_donvikhoan = @id
END;
GO
/****** Object:  StoredProcedure [dbo].[sogio_lamviec_trongtuan]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sogio_lamviec_trongtuan]
	(
		@day date
	)
	as
	begin 
		declare @week int
		set @week = DATEPART(week, @day)
		if(@week % 2 = 0) -- tuần chăn
			begin
				select ma_congnhan,hoten,tong_sogio_lamviec as N'Số giờ làm việc/Tuần chẵn'
				from (
					select cn.ma_congnhan, cn.hoten, sum(nc.sogio_lamviec) as tong_sogio_lamviec
					from NKSLK_CONGNHAN nc, CONGNHAN cn
					where nc.ma_congnhan = cn.ma_congnhan
						  and DATEPART(week, thoigian_batdau) = @week
					group by cn.ma_congnhan, cn.hoten
					) tb_tong_sogio_lamviec
				group by ma_congnhan,hoten, tong_sogio_lamviec
				having tong_sogio_lamviec > 16 -- Insert đủ các ngày trong tuần đó thì sẽ show được, không phải 16 nữa mà là 44 giờ ( 16 - để tạm điều kiện thôi)
			end
		else       -- tuần lẻ
			begin
				select ma_congnhan,hoten,tong_sogio_lamviec as N'Số giờ làm việc/Tuần lẻ'
				from (
					select cn.ma_congnhan, cn.hoten, sum(nc.sogio_lamviec) as tong_sogio_lamviec
					from NKSLK_CONGNHAN nc, CONGNHAN cn
					where nc.ma_congnhan = cn.ma_congnhan
						  and DATEPART(week, thoigian_batdau) = @week
					group by cn.ma_congnhan, cn.hoten
					) tb_tong_sogio_lamviec
				group by ma_congnhan,hoten, tong_sogio_lamviec
				having tong_sogio_lamviec > 8  -- không phải 8 mà là n :
			end
	end
GO
/****** Object:  StoredProcedure [dbo].[songay_lamviec_theothang]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[songay_lamviec_theothang]
	(
		@macongnhan int,
		@month int
	)
	as
	begin 
			select ma_congnhan,hoten,sum(songay_lamviec) as N'Tổng số ngày làm việc'
			from (
						select cn.ma_congnhan,
							   cn.hoten,
							   count(thoigian_batdau)*1.3 as songay_lamviec
						from NKSLK_CONGNHAN nc,
							 CONGNHAN cn
						where (@month IS NULL OR month(thoigian_batdau) = @month)  
							  and DATEPART(HOUR, thoigian_batdau) >= 22
							  and nc.ma_congnhan = cn.ma_congnhan
							  and @macongnhan IS NULL OR cn.ma_congnhan = @macongnhan
						group by cn.ma_congnhan,cn.hoten
					union all
						select cn.ma_congnhan,
							   cn.hoten,
							   count(thoigian_batdau) as songay_lamviec
						from NKSLK_CONGNHAN nc,
							 CONGNHAN cn
						where (@month IS NULL OR month(thoigian_batdau) = @month)  
							  and DATEPART(HOUR, thoigian_batdau) < 22
							  and nc.ma_congnhan = cn.ma_congnhan
							  and @macongnhan IS NULL OR cn.ma_congnhan = @macongnhan
						group by cn.ma_congnhan,cn.hoten
			) tb_songay_lamviec
			group by ma_congnhan,hoten

	end
GO
/****** Object:  StoredProcedure [dbo].[Them_NKSLK_CONGNHAN]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Them_NKSLK_CONGNHAN](
		@ma_nkslk int,
		@ma_congnhan int,
		@thoigian_batdau datetime2(7),
		@thoigian_ketthuc datetime2(7),
		@sogio_lamviec float)
as
begin 
	DECLARE @temp INT, @i INT
    IF((SELECT COUNT(ma_chitiet_dmcongnhan)					
    FROM NKSLK_CONGNHAN) = 0)
		SET @temp=0
	ELSE 
		SET @temp=( SELECT MAX(CAST(ma_chitiet_dmcongnhan AS INT))
    FROM NKSLK_CONGNHAN )
    SET @temp=@temp+1
	SET @i=@temp
   if not exists (select *from NKSLK_CONGNHAN where ma_nkslk= @ma_nkslk and ma_congnhan = @ma_congnhan) -- nkslk đã có công nhân này rồi thì không thêm lại, thêm sẽ trùng dữ liệu
	   begin
			INSERT INTO [dbo].NKSLK_CONGNHAN (ma_chitiet_dmcongnhan,[ma_nkslk],ma_congnhan,thoigian_batdau,thoigian_ketthuc,sogio_lamviec)
			  VALUES
						(@i,@ma_nkslk,@ma_congnhan,@thoigian_batdau,@thoigian_ketthuc,@sogio_lamviec)  
	   end
   else
	   begin
			print N'Chi tiết dm công nhân có ' + convert(nvarchar(10), @ma_nkslk) + N' và ' + convert(nvarchar(10), @ma_congnhan) + N' này đã tồn tại !!';
	   end
end
GO
/****** Object:  StoredProcedure [dbo].[Them_NKSLK_CONGVIEC]    Script Date: 11/7/2021 10:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Them_NKSLK_CONGVIEC](
		@ma_nkslk int,
		@ma_congviec int,
		@ma_sanpham int,
		@soluongthucte int,
		@solosanpham int)
as
begin 
	DECLARE @temp INT, @i INT
    IF((SELECT COUNT(ma_chitietcongviec)					
    FROM NKSLK_CONGVIEC) = 0)
		SET @temp=0
	ELSE 
		SET @temp=( SELECT MAX(CAST(ma_chitietcongviec AS INT))
    FROM NKSLK_CONGVIEC )
    SET @temp=@temp+1
	SET @i=@temp
   if not exists (select *from NKSLK_CONGVIEC where ma_nkslk=@ma_nkslk and ma_congviec = @ma_congviec) -- nkslk đã có công việc này rồi thì không thêm lại, thêm sẽ trùng dữ liệu
	   begin
			INSERT INTO [dbo].[NKSLK_CONGVIEC] ([ma_chitietcongviec],[ma_nkslk],[ma_congviec],[ma_sanpham],[soluongthucte],[solosanpham])
			  VALUES
						(@i,@ma_nkslk,@ma_congviec,@ma_sanpham,@soluongthucte,@solosanpham)  
	   end
   else
	   begin
			print N'Chi tiết công việc có ' + convert(nvarchar(10), @ma_nkslk) + N' và ' + convert(nvarchar(10), @ma_congviec) + N' này đã tồn tại !!';
	   end
end
GO

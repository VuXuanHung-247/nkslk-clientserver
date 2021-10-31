use Nhom6_NKSLK
go

--proc lấy hết công việc
CREATE Procedure Proc_GetAllJob
AS
BEGIN
select * from CONGVIEC
END;
--exec Proc_GetAllJob

--proc lấy công việc theo giá
CREATE Procedure Proc_GetJobBySalary(@salary decimal)
AS
BEGIN
select * from CONGVIEC where dongia = @salary order by tencongviec
END;
--exec Proc_GetJobBySalary '252720'

--proc lấy công việc theo khoảng giá
CREATE Procedure Proc_GetJobByMutilSalary(@salaryMin decimal,@salaryMax decimal)
AS
BEGIN
select * from CONGVIEC where dongia  between @salaryMin and @salaryMax order by tencongviec
END;
--exec Proc_GetJobByMutilSalary 192067 , 252720

-- Lấy ra công việc có nhiều NKSQL nhất
CREATE Procedure Proc_GetMaxJobByNKSLK 
AS
BEGIN
create table #Temp1
(
    ma_congviec int, 
    soluong int
)
insert into #Temp1 select ma_congviec,count(ma_nkslk) soluong from NKSLK_CONGVIEC
group by ma_congviec

create table #Temp2
(
    ma_congviec int, 
    soluong int
)
insert into #Temp2 select ma_congviec,count(ma_nkslk) as soluong from NKSLK_CONGVIEC
group by ma_congviec
Having count(ma_nkslk)  = (select max(soluong) from #Temp1);

select  #Temp2.ma_congviec,CONGVIEC.tencongviec,#Temp2.soluong from #Temp2 inner join CONGVIEC on #Temp2.ma_congviec = CONGVIEC.ma_congviec
END;

--exec Proc_GetMaxJobByNKSLK




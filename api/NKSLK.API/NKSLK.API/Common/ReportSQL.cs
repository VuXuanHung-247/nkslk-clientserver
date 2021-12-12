using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Common
{
    public class ReportSQL
    {
		// Nhật ký sản lượng khoán làm riêng
		public static string queryReportBySelf = @"select n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, nc.thoigian_batdau, nc.thoigian_ketthuc
	                        from NKSLK n, NKSLK_CONGNHAN nc, CONGNHAN cn
	                        where n.ma_nkslk = nc.ma_nkslk
	                             and nc.ma_congnhan = cn.ma_congnhan
		                         and nc.ma_nkslk in (select nc.ma_nkslk
							                         from NKSLK_CONGNHAN nc
							                         group by  nc.ma_nkslk
							                         having COUNT(ma_congnhan) = 1)
	                        group by n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, nc.thoigian_batdau, nc.thoigian_ketthuc
	                        order by n.ngaybatdau desc
                            ";
		// Nhật ký sản lượng khoán làm chung
		public static string queryReportByTogether = @"select n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, nc.thoigian_batdau, nc.thoigian_ketthuc
	                        from NKSLK n, NKSLK_CONGNHAN nc, CONGNHAN cn
	                        where n.ma_nkslk = nc.ma_nkslk
	                             and nc.ma_congnhan = cn.ma_congnhan
		                         and nc.ma_nkslk in (select nc.ma_nkslk
							                         from NKSLK_CONGNHAN nc
							                         group by  nc.ma_nkslk
							                         having COUNT(ma_congnhan) > 1)
	                        group by n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, nc.thoigian_batdau, nc.thoigian_ketthuc
	                        order by n.ngaybatdau desc
                            ";
		// Nhật ký sản lượng khoán làm muộn
		public static string queryReportByLate = @"select n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.thoigian_batdau, nc.thoigian_batdau, n.thoigian_ketthuc, DATEDIFF(minute, n.thoigian_batdau, nc.thoigian_batdau) as sophutdimuon
	                        from NKSLK n, NKSLK_CONGNHAN nc, CONGNHAN cn
	                        where n.ma_nkslk = nc.ma_nkslk
	                             and nc.ma_congnhan = cn.ma_congnhan
		                         and DATEDIFF(minute, n.thoigian_batdau, nc.thoigian_batdau) > 0
		                         and nc.ma_nkslk in (select nc.ma_nkslk
							                         from NKSLK_CONGNHAN nc
							                         group by  nc.ma_nkslk
							                         having COUNT(ma_congnhan) >= 1)
	                        group by  n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.thoigian_batdau, nc.thoigian_batdau, n.thoigian_ketthuc
	                        order by  n.thoigian_batdau desc
                            ";
		// Số ngày làm việc (theo tháng/công nhân)
		public static string queryGetWorkingDays = @"select ma_congnhan,hoten,sum(songay_lamviec) as tongngaylamviec
			                from (
						                select nc.ma_congnhan,
							                   cn.hoten,
							                   count(thoigian_batdau)*1.3 as songay_lamviec
						                from NKSLK_CONGNHAN nc,
							                 CONGNHAN cn
						                where (@month IS NULL OR month(thoigian_batdau) = @month)  
							                  and DATEPART(HOUR, thoigian_batdau) >= 22
							                  and nc.ma_congnhan = cn.ma_congnhan
							                  and (@macongnhan IS NULL OR cn.ma_congnhan = @macongnhan) 
						                group by nc.ma_congnhan,cn.hoten
					                union all
						                select nc.ma_congnhan,
							                   cn.hoten,
							                   count(thoigian_batdau) as songay_lamviec
						                from NKSLK_CONGNHAN nc,
							                 CONGNHAN cn
						                where (@month IS NULL OR month(thoigian_batdau) = @month)  
							                  and DATEPART(HOUR, thoigian_batdau) < 22
							                  and nc.ma_congnhan = cn.ma_congnhan
							                  and (@macongnhan IS NULL OR cn.ma_congnhan = @macongnhan)
						                group by nc.ma_congnhan,cn.hoten
			                ) tb_songay_lamviec
			                group by ma_congnhan,hoten
                            ";


		public static string queryGetExpiredProducts = @"select * from SANPHAM where DATEDIFF(day,ngaydangky,hansudung)=0";

	}
}

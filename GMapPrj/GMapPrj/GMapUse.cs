using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMAPNS;
using GMapPrj;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GMapNS
{
    class GMapUse
    {
        GMAP m_mapCtl = null;
        public void Init(GMAP mapCtl)
        {
            m_mapCtl = mapCtl;


            //System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("ditu.google.cn");
            //mapControl.MapProvider = GMapProviders.GoogleChinaMap; 
     
            m_mapCtl.MapProvider = AMapProvider.Instance;

            //m_mapCtl.Manager.Mode = AccessMode.CacheOnly;
            m_mapCtl.Manager.Mode = AccessMode.ServerAndCache;

            m_mapCtl.Position = new PointLatLng(39, 115); 

            m_mapCtl.MinZoom = 4;  //最小缩放
            m_mapCtl.MaxZoom = 18; //最大缩放
            m_mapCtl.Zoom = 11;     //当前缩放


            m_mapCtl.ShowCenter = false; //不显示中心十字点
            m_mapCtl.DragButton = MouseButton.Left; //左键拖拽地图

            //new Thread(() => GMaps.Instance.ImportFromGMDB(@"C:\Data.gmdb")).Start();
            
            m_mapCtl.MouseRightButtonDown += mapControl_MouseLeftButtonDown;
        }

        private void DownLoadMap()
        {//C:\Users\NNNNN\AppData\Local\GMap.NET\TileDBv5\zh-CN
            RectLatLng area = RectLatLng.FromLTRB(104.28077393000, 26.58757324030, 112.25690002000, 20.70236091970);
            if (!area.IsEmpty)
            {
                try
                {
                    for (int i = 10; i <= 11; i++)
                    {
                        GMap.NET.WindowsPresentation.TilePrefetcher obj = new GMap.NET.WindowsPresentation.TilePrefetcher();
                        obj.Owner = MainWindow.obj;
                        obj.ShowCompleteMessage = true;
                        obj.Start(area, i, m_mapCtl.MapProvider, 100);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(m_mapCtl);
            PointLatLng point = m_mapCtl.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);

            AddMarker(point, System.AppDomain.CurrentDomain.BaseDirectory + "mark.png", "xxx");
        }

        void AddMarker(PointLatLng pt, string imgPathName, string title)
        {
            GMapMarker mk = new GMapMarker(pt);
            mk.Shape = new UserMarker(m_mapCtl, mk, imgPathName, title);
            m_mapCtl.Markers.Add(mk);


            m_pointList.Add(pt);
            AddRout(m_pointList);
        }


        List<PointLatLng> m_pointList = new List<PointLatLng>();
        void AddRout(List<PointLatLng> pointList)
        {
            GMapRoute route = new GMapRoute(pointList);
            m_mapCtl.Markers.Add(route);
        }
        public void RemoveAll()
        {
            m_mapCtl.Markers.Clear();
        }

    }
}

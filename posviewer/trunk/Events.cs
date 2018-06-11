using System;
using System.Collections.Generic;
using System.Text;
using Maple.Global1.G1BizObjects;

namespace Maple.Dtc.PositionClient
{
    public delegate void IncomingDtcMessageEventHandler(object sender, IncomingDtcMessageEventArgs e);
    public delegate void IncomingG1MessageEventHandler(object sender, IncomingG1MessageEventArgs e);

    public delegate void CusipSelectEventHandler(object sender, CusipSelectEventArgs e);

    public delegate void UserJoinEventHandler(object sender, UserEventArgs e);
    public delegate void UserLeaveEventHandler(object sender, UserEventArgs e);

    public delegate void StockInfoUpdateEventHandler(object sender, StockInfoUpdateEventArgs e);
    public delegate void StockPriceUpdateEventHandler(object sender, StockPriceUpdateEventArgs e);

    public delegate void PositionUpdateEventHandler(object sender, PositionUpdateEventArgs e);
    

    public class PositionUpdateEventArgs : EventArgs
    {
        private RealTimePositionObject _p;

        public RealTimePositionObject Position
        {
            get { return _p; }
            set { _p = value; }
        }

        public PositionUpdateEventArgs(RealTimePositionObject pos)
        {
            Position = pos;
        }
    }

    public class IncomingDtcMessageEventArgs : EventArgs
    {
        private IDtcMessage _deliveryOrder;

        public IDtcMessage DtcMessage
        {
            get { return _deliveryOrder; }
            set { _deliveryOrder = value; }
        }

        public IncomingDtcMessageEventArgs(IDtcMessage msg)
        {
            DtcMessage = msg;
        }
    }

    public class IncomingG1MessageEventArgs : EventArgs
    {
        public G1RealTimeActivityObject G1Activity {get; set;}

        public IncomingG1MessageEventArgs(G1RealTimeActivityObject msg)
        {
            G1Activity = msg;
        }
    }



    public class CusipSelectEventArgs : EventArgs
    {
        private string _cusip;

        public string Cusip
        {
            get { return _cusip; }
            set { _cusip = value; }
        }

        public CusipSelectEventArgs(string cusip)
        {
            Cusip = cusip;
        }
    }

    public class StockInfoUpdateEventArgs : EventArgs
    {
        private StockInfoObject _si;

        public StockInfoObject StockInfo
        {
            get { return _si; }
            set { _si = value; }
        }

        public StockInfoUpdateEventArgs(StockInfoObject si)
        {
            StockInfo = si;
        }
    }

    public class StockPriceUpdateEventArgs : EventArgs
    {
        private StockPriceObject _sp;

        public StockPriceObject StockPrice
        {
            get { return _sp; }
            set { _sp = value; }
        }

        public StockPriceUpdateEventArgs(StockPriceObject sp)
        {
            StockPrice = sp;
        }
    }


    public class UserEventArgs : EventArgs
    {
        private string _member;

        public string Member
        {
            get { return _member; }
            set { _member = value; }
        }

        public UserEventArgs(string member)
        {
            Member = member;
        }
    }
        
}

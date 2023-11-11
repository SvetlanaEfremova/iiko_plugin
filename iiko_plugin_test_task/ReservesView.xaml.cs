using Resto.Front.Api.Data.Brd;
using Resto.Front.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Moq;
using Resto.Front.Api.Data.Orders;
using Resto.Front.Api.Data.Organization.Sections;

namespace iiko_plugin_test_task
{
    public sealed partial class ReservesView
    {
        private readonly ObservableCollection<IReserve> reserves = new ObservableCollection<IReserve>();

        public ObservableCollection<IReserve> Reserves
        {
            get { return reserves; }
        }

        public ReservesView()
        {
            InitializeComponent();
            ReloadReserves();
        }

        internal void ReloadReserves()
        {
            reserves.Clear();
            var mockData = new List<IReserve>();
            var mockReserve = new Mock<IReserve>();
            mockReserve.Setup(r => r.EstimatedStartTime).Returns(DateTime.Now.AddHours(1));
            mockReserve.Setup(r => r.GuestsCount).Returns(4);
            mockReserve.Setup(r => r.Comment).Returns("Test comment");
            mockReserve.Setup(r => r.Status).Returns(ReserveStatus.New);
            mockReserve.Setup(r => r.Tables).Returns(new List<ITable>());

            var mockClient = new Mock<IClient>();
            mockClient.Setup(c => c.Name).Returns("Mr. Smith");
            mockReserve.Setup(r => r.Client).Returns(mockClient.Object);

            var mockOrder = new Mock<IOrder>();
            mockReserve.Setup(r => r.Order).Returns(mockOrder.Object);

            IReserve reserve = mockReserve.Object;
            mockData.Add(reserve);
            mockData.ForEach(reserves.Add);
        }
    }
}

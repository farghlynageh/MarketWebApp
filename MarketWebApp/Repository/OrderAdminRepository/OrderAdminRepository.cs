
//using MarketWebApp.Data;
//using MarketWebApp.Models.Entity;
//using MarketWebApp.ViewModel.Order;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;

//namespace MarketWebApp.Reprository.OrderReprository
//{
//    public class OrderRepository : IOrderRepository
//    {
//        private readonly ApplicationDbContext context;

//        public OrderRepository(ApplicationDbContext context)
//        {
//            this.context = context;
//        }
//        public void Delete(int id)
//        {
//            var Order = GetOrder(id);
//            context.Orders.Remove(Order);
//            Save();
//        }
//        public IEnumerable<Order> GetAll()
//        {
//            return context.Orders.Include(b => b.OrderProducts).Include(c => c.User).ToList();
//        }
//        public Order GetOrder(int Id)
//        {
//            return context.orders.Include(p=>p.subOrders).SingleOrDefault(c => c.Id == Id);
//        }
//        public Order GetOrderWithSubOrders(int Id)
//        {
//            return context.orders.Where(c => c.Id == Id).Include(c => c.subOrders).ThenInclude(p=>p.Product).SingleOrDefault();

//        }
//        public void Insert(AddOrderViewModel addOrderViewModel)
//        {
//            var order = new Order();
//            order.ResevationDate = addOrderViewModel.ResevationDate;
//            order.DeliveryDate = addOrderViewModel.DeliveryDate;
//            order.Cost = addOrderViewModel.Cost;
//            order.destination = addOrderViewModel.destination;
//            order.IsConfirmed = addOrderViewModel.IsConfirmed;
//            order.UserId = addOrderViewModel.UserId;
//            context.orders.Add(order);
//        }
//        public void Save()
//        {
//            context.SaveChanges();
//        }
//        public void Update(EditOrderViewModel editOrderViewModel)
//        {
//            var order = GetOrder(editOrderViewModel.Id);
//            order.ResevationDate = editOrderViewModel.ResevationDate;
//            order.DeliveryDate = editOrderViewModel.DeliveryDate;
//            order.Cost = editOrderViewModel.Cost;
//            order.destination = editOrderViewModel.destination;
//            order.IsConfirmed = editOrderViewModel.IsConfirmed;
//            order.UserId = editOrderViewModel.UserId;
//        }


//        public Order InsertNew(AddOrderViewModel addOrderViewModel)
//        {
//            var order = new Order();
//            order.ResevationDate = addOrderViewModel.ResevationDate;
//            order.DeliveryDate = addOrderViewModel.DeliveryDate;
//            order.Cost = addOrderViewModel.Cost;
//            order.destination = addOrderViewModel.destination;
//            order.IsConfirmed = addOrderViewModel.IsConfirmed;
//            order.UserId = addOrderViewModel.UserId;
//            context.orders.Add(order);
//            context.SaveChanges();
//            return order;
//        }

//        IEnumerable<Order> IOrderRepository.GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        Order IOrderRepository.GetOrder(int Id)
//        {
//            throw new NotImplementedException();
//        }

//        Order IOrderRepository.GetOrderWithSubOrders(int Id)
//        {
//            throw new NotImplementedException();
//        }

//        public void Insert(AddOrderViewModel addOrderViewModel)
//        {
//            throw new NotImplementedException();
//        }

//        public void Update(EditOrderViewModel editOrderViewModel)
//        {
//            throw new NotImplementedException();
//        }

//        public Order InsertNew(AddOrderViewModel addOrderViewModel)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

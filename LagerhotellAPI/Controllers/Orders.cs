﻿using LagerhotellAPI.Models.DomainModels.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("/orders")]

    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private OrderValidator _orderValidator;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
            _orderValidator = new OrderValidator();
        }

        /// <summary>
        /// Adds an order to the database, and returns the orderId
        /// </summary>
        /// <param name="request"></param>
        /// <returns>orderId</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderRequest request)
        {
            var validationResult = _orderValidator.Validate(request.Order);
            if (validationResult.IsValid)
            {
                string orderId = await _orderService.AddOrder(request.Order);
                return Ok(new AddOrderResponse(orderId));
            }
            else
            {
                Console.WriteLine($"The request order did not meet the validators requirements, {validationResult.Errors}");
                return BadRequest($"The request order did not meet the validators requirements, {validationResult.Errors}");
            }
        }

        /// <summary>
        /// Deletes an order with the given orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Success bool</returns>
        // DeleteOrder
        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder([FromRoute] string orderId)
        {
            try
            {
                var order = _orderService.DeleteOrder(orderId);
                return Ok(new DeleteOrderResponse { Success = true });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in GetAllOrders: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets an order with the given orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Order domain object</returns>
        // GetOrderById
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute] string orderId)
        {
            try
            {
                var order = await _orderService.GetOrder(orderId);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in GetOrderById: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets all orders in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>A list of all orders</returns>
        // GetAllOrders
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders([FromQuery] string? userId, int? skip, int? take, OrderStatus? orderStatus)
        {
            // rework logic
            try
            {
                var orders = await _orderService.GetAllOrders(userId, skip, take, orderStatus);
                return Ok(orders);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in GetAllOrders: {e}");
                return StatusCode(500);
            }
        }

        // GetAllOrdersByUser
        /* [HttpGet("byuser")]
        public IActionResult GetAllOrdersByUser([FromQuery] User user)
        {
            try
            {
                var orders = _orderService.GetAllOrdersByUser(user);
                if (orders != null)
                {
                    return Ok(orders);
                }
                return NotFound();
            }
            catch
            {
                return Conflict();
            }
        } */
    }
}
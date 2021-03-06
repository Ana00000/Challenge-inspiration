using System;

namespace ExamplesApp.Method
{
    class Payment
    {
    	public int Cost { get; set; }
    	public bool IsExtra { get; set; }
    }

    class PaymentService{
	/// <summary>
        /// 1) Extract createPayment method.
        /// </summary>
    	private void CreatePayment(int price, int compensation) {
		Payment payment = new Payment();
		payment.Cost = price + compensation;
		payment.IsExtra = payment.Cost > 50000 ? true : false;

      		// Print details.
      		System.out.println("Hello.");
      		System.out.println("Your payment is created.");
      		System.out.println("Cost is: " + payment.Cost);
    	}
    }
}

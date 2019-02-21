using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class Purchasing : MonoBehaviour, IPurchasingAdapter
{
    //This method is called when the Purcahsing Adapter is set, before or after Monetization is initialized
    //Monetization.SetPurchasingAdapter()
    public void RetrieveProducts(IRetrieveProductsListener listener)
    {
        //Configure products into Monetization Product format
        List<Product> products = new List<Product>();
        products.Add(new Product {
            productId = "gold_coins_100",
            localizedTitle = "100 Gold Coins",
            localizedDescription = "Awesome Gold Coins for a new low price!",
            localizedPriceString = "$0.99",
            isoCurrencyCode = "USD",
            productType = "Consumable",
            localizedPrice = 0.99m
        });

        //Provide the retrieved Products list to the Monetization system:
        listener.OnProductsRetrieved(products);
    }

    //This method is called when a player clicks on the IAP Promo
    //A developer will add the hooks to their purchasing system here to complete the purchase
    public void Purchase(string productID, ITransactionListener listener, IDictionary<string, object> extras)
    {

        // When ThirdPartyPurchasing succeeds:
        listener.OnTransactionComplete(new TransactionDetails
        {
            currency = "USD",
            price = 1.99m,
            productId = "100bronzeCoins",
            transactionId = "", //Transaction ID from successful puchase
            receipt =
                "{\n\"data\": \"{\\\"Store\\\":\\\"fake\\\",\\\"TransactionID\\\":\\\"ce7bb1ca-bd34-4ffb-bdee-83d2784336d8\\\",\\\"Payload\\\":\\\"{ \\\\\\\"this\\\\\\\": \\\\\\\"is a fake receipt\\\\\\\" }\\\"}\"\n}"
        });

        //When ThirdPartyPurchasing fails:
        //Fill in appropriate details from Purchasing system
        listener.OnTransactionError(new TransactionErrorDetails
        {
            transactionError = TransactionError.NetworkError,
            exceptionMessage = "Test exception message",
            store = Store.GooglePlay,
            storeSpecificErrorCode = "Example: Google Play lost connection",
        });
    }
}

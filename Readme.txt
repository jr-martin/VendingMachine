Vending Machine
----------------------------------------------------------------------------------------------------------------------
This console application simulates a simple vending machine. The machine displays a list of items along with each item's corresponding
price, amount, and item code. To use, simply run the executable or debug the project and follow the prompts.


How To Use
----------------------------------------------------------------------------------------------------------------------
The vending machine funds and inventory are configured through two yaml files: Inventory.yaml and Money.yaml. Both are located
in the ConfigFiles folder, which is located at the root directory of the solution. The application will automatically use the
values in these two files for its inventory and funds, and both have been configured with default values.

Please see the comments on the files themselves for more information on how they are structured.


Assumptions and Limitations
----------------------------------------------------------------------------------------------------------------------
- This vending machine only takes coins, not bills (but it will take one dollar coins).
- This machine is only configured for US currency.
- The machine will not accept coin denominations that it was not initially stocked with, even if they are valid currency.
- This machine can be stocked with coins that don't exist (e.g. a 15 cent coin).
- Inventory and Funds cannot be restocked while the machine is running.
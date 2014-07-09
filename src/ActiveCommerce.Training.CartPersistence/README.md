Cart Persistence POC
========
This a proof of concept example which demonstrates how you might implement persistent carts in Active Commerce.

It provides pipelines for persisting and loading the cart, and a pipeline for restoring individual products on load.
It implements persistence both via cookie and via customer data (for signed in users). There is also a simple mechanism
for alerting the user about successful and failed cart restore via the shopping cart alert message.

Experimental, use at your own risk.
Failed Payment Message
======================

This simple enhancement makes the message presented to the user on failed
payment more informative, by adding the payment provider's message to the
message in the order processing pipeline. Note that you will want to test
this with various failed payment scenarios with your payment gateway, to
ensure that the messages returned are indeed helpful. Alternatively, you can
check the status code from your gateway and return a custom message. If you
utilize a dictionary key as your message, it will automatically be translated
into the corresponding phrase from the translation dictionary.
# Overview

This is a copy of [regis-pay](https://github.com/reggieray/regis-pay) that has been updated for higher throughput with faster processing.

The following changes were made:

1. Add [Mediator](https://github.com/martinothamar/Mediator)
    - reduce outbox pattern for every integration event.
    - Use Mediator command pattern and add state checks to avoid reprocessing the same event twice.
    - Outbox pattern only exists for PaymentInitiated event. 
2. Configure ChangeFeed
    - Update publishing of messages to be done in batches
    - Update ChangeFeed settings to pull and process more changes at once.
3. Configure Consumer
   - Update PaymentInitiated to handle more concurrent messages and to prefetch more
4. Add Metrics
   - Created metrics to track performance and make available on Aspire dashboard. 

# Testing

I'm going to use my own abbreviation `PPS`, which is short for `payments per second` to describe the tests below.

1. `100 pps`
2. `50 pps`
3. `60 pps`
4. `70 pps`
5. `80 pps`
6. `75 pps`

<img width="1141" height="676" alt="image" src="https://github.com/user-attachments/assets/15dd4521-c439-46c6-89f1-428e7da24ebc" />


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

1. `100 pps` - Too high a load, this created a build of payments that grew exponentially. As you can see the later payments took 60 seconds to process. 
2. `50 pps` - Tried half the previous load and it seemed to cope just fine.
3. `60 pps` - Same as above.
4. `70 pps` - Same as above, although the time it took per payment was averaging 1-3 sec, where previously it was under 1 sec.
5. `80 pps` - This seemed to be one step too far, although not as bad a `100 pps` there was a notably difference where payments were building up and taking time to be processed.
6. `75 pps` - Adjusted it down `5 pps` from the previous test run which reduced the build up of payments. It's worth noting it was looking like payments were averaging around 5 sec.

<img width="1141" height="676" alt="image" src="https://github.com/user-attachments/assets/15dd4521-c439-46c6-89f1-428e7da24ebc" />


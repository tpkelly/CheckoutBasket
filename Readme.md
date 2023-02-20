== Design decisions ==
- The handling of the basket was pulled out from the controller into a BasketManager, to allow the basket to remain between HTTP requests in the swagger portal. On each request, the controller is created again from scratch, so anything that was added in a previous request needs to be held outside of the controller. Ideally this would be tied to a user session so different users don't share the same basket, but this was skipped to avoid overcomplicating the exercise
- Writing tests before the code allows us to think about the bigger picture, as well as letting us know when it is done. It also helps to see the test failing before we write the code, so we know the test is testing for what we want
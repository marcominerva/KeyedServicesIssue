# Issue with FromKeyedServices attribute

If we use the `FromKeyedServices` attribute in a class library and the keyed registration is missing, it falls back to the default dependency injection behavior and gets the last registered implementation. However, If we use `FromKeyedServices` directly in a Minimal API endpoint handler and the key does not exist, it throws an exception.

This repo contains some examples that showcase all the different scenarios. Every endpoint comes with a description of the particular behavior it refers to.

![image](https://github.com/marcominerva/KeyedServicesIssue/assets/3522534/452daf1e-ac28-4a8d-8fd9-1d688bbd4da5)

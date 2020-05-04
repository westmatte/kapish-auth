Connect-AzureAD -TenantId ""

#Where ObjectId and Principal Id is the objectId of the system/user-assigned identity.
#Where Id is the guid of the app role in the app registration of which it is trying to access
#Where ResourceId is the app registration objectId of the app registration it is trying to access

New-AzureADServiceAppRoleAssignment -ObjectId "" -Id "" -PrincipalId "" -ResourceId ""
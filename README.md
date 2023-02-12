# AuthService

The service provides logic for managing user sessions using jwt-auth.
The service provides functionality to create user, authenticate, get user and refresh token.  
During authentication user take access token and refresh token, and can refresh it. Also service will provide additional methods, like update password, reset password, etc.

| Method       | Description                                                                  |
| ------------ | ---------------------------------------------------------------------------- |
| Authenticate | Returns access and refresh token if received email and password are correct. |
| Refresh      | Create access and refresh token using existing not expired refresh token.    |
| GetUser      | Get user information by userId.                                              |
| CreateUser   | Creates new user with email, password, roles and claims.                     |

<h1>Database entities: </h1>

| User                |
| ------------------- |
| id                  |
| email               |
| password_hash       |
| lockout_end_at_utc  |
| lockout_enabled     |
| access_failed_count |
| created_at_utc      |
| updated_at_utc      |
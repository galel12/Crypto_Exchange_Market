export interface AuthState {
  user: { username: string; email?: string } | null; // Define user type
  token: string | null; // Define token type
  successMessage: string; // Add successMessage
  errorMessage: string; // Add errorMessage
}

export function state(): AuthState {
  return {
    user: null, // Initially no user is logged in
    token: null, // No token at the start
    successMessage: "", // Default success message
    errorMessage: "", // Default error message
  };
}
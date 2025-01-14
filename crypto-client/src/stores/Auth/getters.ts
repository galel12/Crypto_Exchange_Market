import { type AuthState } from "./state";

export const getters = {
  isAuthenticated(state: AuthState): boolean {
    return !!state.token; // Return true if a token exists
  },
  getUser(state: AuthState): AuthState["user"] {
    return state.user; // Return user details
  },
};
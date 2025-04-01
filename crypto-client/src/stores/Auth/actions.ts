import { type AuthStore } from "./index";
import { type AuthState } from "./state";

export const actions: {
  login(
    this: AuthStore,
    payload: { username: string; password: string }
  ): Promise<void>;
  signUp(
    this: AuthStore,
    payload: { username: string; password: string }
  ): Promise<void>;
  logout(this: AuthStore): void;
} = {
  async login(
    this: AuthStore,
    payload: { username: string; password: string }
  ): Promise<void> {
    try {
      const response = await fetch("http://localhost:5040/api/Auth", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      const data = await response.json();

      if (!response.ok) {
        console.log(data);
        throw new Error(data.message || "Login failed");
      }

      this.token = data.accessToken; // Update to use accessToken
      const refreshToken = data.refreshToken; // Store refreshToken

      if (this.token && refreshToken) {
        localStorage.setItem("token", this.token);
        localStorage.setItem("refreshToken", refreshToken); // Save refresh token

      } else {
        console.warn("Token is null, not saving to localStorage");
      }

      // Update success message
      this.successMessage = "Login successful! Redirecting to your wallet...";
    } catch (error: unknown) {
      if (error instanceof Error) {
        this.errorMessage = error.message;
      } else {
        this.errorMessage = "An unexpected error occurred.";
      }
      throw error;
    }
  },

  async signUp(
    this: AuthStore,
    payload: { username: string; password: string }
  ): Promise<void> {
    try {
      const response = await fetch("http://localhost:5040/api/User", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        const errorDetails = await response.json();
        throw new Error(errorDetails.message || "Sign-up failed");
      }

      const data = await response.json();

      this.user = {
        username: data.username,
      };

      localStorage.setItem('userName', this.user.username);
      
      // Update success message
      this.successMessage = "Sign-up successful! You can now log in.";
    } catch (error: unknown) {
      if (error instanceof Error) {
        this.errorMessage = error.message;
      } else {
        this.errorMessage = "An unexpected error occurred.";
      }
      throw error;
    }
  },

  logout(this: AuthStore): void {
    this.user = null;
    this.token = null;
    localStorage.removeItem("token"); // Clear token from localStorage
    localStorage.removeItem("userName"); // Clear user Name from localStorage
    localStorage.removeItem("refreshToken"); // Clear refreshToken from localStorage
  },
};
import axios from "axios";

// Set up an Axios instance with base configuration
const apiClient = axios.create({
  baseURL: "http://localhost:5040/api", // Base URL for your API
  headers: {
    "Content-Type": "application/json",
  },
});

// Function to handle login requests
export const loginUser = async (data: { username: string; password: string }) => {
  const response = await apiClient.post("/Auth", data); // Adjusting to `/api/Auth` for login
  return response.data; // Return response payload
};

// Function to handle sign-up requests
export const signUpUser = async (data: { username: string; password: string }) => {
  const response = await apiClient.post("/User", data); // Adjusting to `/api/User` for sign-up
  return response.data; // Return response payload
};
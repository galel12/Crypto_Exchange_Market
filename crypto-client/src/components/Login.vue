<template>
  <div class="login-container">
    <h2>Login</h2>
    <!-- Error Message -->
    <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
    <!-- Success Message -->
    <p v-if="successMessage" class="success-message">{{ successMessage }}</p>
    <form @submit.prevent="handleLogin">
      <p>
        <label for="username">Username:</label>
        <input id="username" v-model="username" type="text" />
      </p>
      <p>
        <label for="password">Password:</label>
        <input id="password" v-model="password" type="password" />
      </p>
      <button type="submit">Login</button>
    </form>
  </div>
</template>

<script lang="ts">
import { ref, computed, onUnmounted } from "vue";
import { useAuthStore } from "@/stores/Auth";
import { useRouter } from "vue-router";

export default {
  name: "Login",
  setup() {
    // State variables
    const username = ref("");
    const password = ref("");

    // Access the Auth store
    const authStore = useAuthStore();

    // Access Vue Router
    const router = useRouter();

    // Computed properties for messages
    const successMessage = computed(() => authStore.successMessage);
    const errorMessage = computed(() => authStore.errorMessage);

    // Handle login logic
    const handleLogin = async () => {
      try {
        await authStore.login({ username: username.value, password: password.value });
        router.push("/wallet"); // Redirect to the wallet after successful login
      } catch (error) {
        console.error("Login failed:", error);
      }
    };

    // Clear messages on component unmount
    onUnmounted(() => {
      authStore.successMessage = "";
      authStore.errorMessage = "";
    });

    // Return state and methods for the template
    return {
      username,
      password,
      successMessage,
      errorMessage,
      handleLogin,
    };
  },
};
</script>

<style scoped>
.login-container {
  width: 100%;
  max-width: 400px;
  padding: 40px;
  border-radius: 20px;
  background-color: white;
  box-shadow: 0px 12px 24px rgba(0, 0, 0, 0.3);
}

h2 {
  text-align: center;
  margin-bottom: 20px;
  color: #333;
  font-size: 32px;
  font-weight: bold;
}

form p {
  margin-bottom: 15px;
}

label {
  display: block;
  margin-bottom: 5px;
  color: #555;
  font-size: 16px;
  font-weight: bold;
}

input {
  width: 100%;
  padding: 12px;
  border: 1px solid #1877f2;
  border-radius: 5px;
  box-sizing: border-box;
  font-size: 18px;
}

button {
  width: 100%;
  padding: 10px;
  background-color: #000000;
  color: #fff;
  border: none;
  border-radius: 10px;
  font-size: 20px;
  cursor: pointer;
  margin-top: 15px;
}

button:hover {
  background-color: #41454b;
}

.error-message {
  color: red;
  font-size: 14px;
  margin-bottom: 10px;
  text-align: center;
}

.success-message {
  color: green;
  font-size: 14px;
  margin-bottom: 10px;
  text-align: center;
}
</style>
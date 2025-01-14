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
import { defineComponent } from "vue";
import { useAuthStore } from "@/stores/Auth";

export default defineComponent({
  name: "Login",
  data() {
    return {
      username: "",
      password: "",
    };
  },
  computed: {
    // Map success and error messages from the store
    successMessage() {
      return this.authStore.successMessage;
    },
    errorMessage() {
      return this.authStore.errorMessage;
    },
  },
  methods: {
    async handleLogin() {
      try {
        await this.authStore.login({
          username: this.username,
          password: this.password,
        });

        // Redirect to the wallet after successful login
        this.$router.push("/wallet");
      } catch (error) {
        console.error("Login failed:", error);
      }
    },
  },
  setup() {
    const authStore = useAuthStore(); // Use the Pinia store
    return { authStore };
  },
  beforeUnmount() {
    // Clear messages when leaving the component
    this.authStore.successMessage = "";
    this.authStore.errorMessage = "";
  },
});
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
  /* Increased size */
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
  /* Increased size */
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
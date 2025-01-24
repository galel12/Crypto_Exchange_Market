<template>
  <div class="signup-container">
    <h2>Sign Up</h2>
    <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
    <p v-if="successMessage" class="success-message">{{ successMessage }}</p>
    <form @submit.prevent="handleSignUp">
      <p>
        <label for="username">Username:</label>
        <input id="username" v-model.trim="username" type="text" />
      </p>
      <p>
        <label for="password">Password:</label>
        <input id="password" v-model.trim="password" type="password" />
      </p>
      <p v-if="!formIsValid">Please enter a valid Username and Password (must be at least 1 character long!) </p> 
      <button type="submit">Sign Up</button>
    </form>
  </div>
</template>

<script lang="ts">
import { ref, computed, onUnmounted } from "vue";
import { useAuthStore } from "../stores/Auth";
import { useRouter } from "vue-router";

export default {
  setup() {
    // State variables
    const username = ref("");
    const password = ref("");
    const formIsValid = ref(true);
    // Access the Auth store
    const authStore = useAuthStore();

    // Access Vue Router
    const router = useRouter();

    // Computed properties for messages
    const successMessage = computed(() => authStore.successMessage);
    const errorMessage = computed(() => authStore.errorMessage);

    // Handle sign-up logic
    const handleSignUp = async () => {
      formIsValid.value = username.value.length > 0 && password.value.length > 0;
      
      if (!formIsValid.value) {
        console.error("Form is invalid. Please fill in all fields.");
        return;
      }
      try {
        await authStore.signUp({ username: username.value, password: password.value });
        router.push("/"); // Redirect to the login page after successful sign-up
      } catch (error) {
        console.error("Sign-up failed:", error);
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
      handleSignUp,
      formIsValid,
    };
  },
};
</script>

<style scoped>
.signup-container {
  width: 100%;
  max-width: 400px;
  padding: 40px;
  border-radius: 20px;
  background-color: white;
  box-shadow: 0px 12px 24px rgba(0, 0, 0, 0.3);
  justify-self: center;
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
  color: white;
  border: none;
  border-radius: 10px;
  font-size: 20px;
  cursor: pointer;
  margin-top: 15px;
}

button:hover {
  background-color: #41454b;
}

.success-message {
  color: green;
  font-size: 14px;
  margin-bottom: 10px;
  text-align: center;
}
</style>
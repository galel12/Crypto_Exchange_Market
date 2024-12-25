<template>
  <div>
    <h1>Login</h1>
    <form @submit.prevent="handleLogin">
      <div>
        <label>Username:</label>
        <input v-model="username" required />
      </div>
      <div>
        <label>Password:</label>
        <input v-model="password" type="password" required />
      </div>
      <button type="submit">Login</button>
    </form>
    <p v-if="message">{{ message }}</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { useRouter } from "vue-router";

export default defineComponent({
  name: "Login",
  setup() {
    const username = ref("");
    const password = ref("");
    const message = ref("");
    const router = useRouter();

    const handleLogin = async () => {
      try {
        const response = await fetch("/api/Auth", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ username: username.value, password: password.value }),
        });

        if (response.ok) {
          const data = await response.json();
          localStorage.setItem("token", data.token); // Save JWT token
          router.push("/wallet"); // Redirect to the wallet page
        } else {
          const error = await response.json();
          message.value = error.message || "Login failed.";
        }
      } catch (err) {
        message.value = "An error occurred. Please try again.";
      }
    };

    return { username, password, message, handleLogin };
  },
});
</script>
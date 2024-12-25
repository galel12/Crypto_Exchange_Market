<template>
  <div>
    <h1>Sign Up</h1>
    <form @submit.prevent="handleSignUp">
      <div>
        <label>Username:</label>
        <input v-model="username" required />
      </div>
      <div>
        <label>Password:</label>
        <input v-model="password" type="password" required />
      </div>
      <button type="submit">Sign Up</button>
    </form>
    <p v-if="message">{{ message }}</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  name: "SignUp",
  setup() {
    const username = ref("");
    const password = ref("");
    const message = ref("");

    const handleSignUp = async () => {
      try {
        const response = await fetch("/api/User", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ username: username.value, password: password.value }),
        });

        if (response.ok) {
          message.value = "Sign up successful! Please log in.";
        } else {
          const error = await response.json();
          message.value = error.message || "Sign up failed.";
        }
      } catch (err) {
        message.value = "An error occurred. Please try again.";
      }
    };

    return { username, password, message, handleSignUp };
  },
});
</script>
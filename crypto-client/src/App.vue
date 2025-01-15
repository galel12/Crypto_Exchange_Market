<template>
  <div id="app" class="app-container">
    <!-- Header -->
    <header class="app-header">
      <h1 class="header-title">Crypto Exchange Marketplace</h1>
      <nav class="navigation">
        <router-link to="/">Login</router-link>
        <router-link to="/signUp">Sign Up</router-link>
        <router-link to="/wallet">Wallet</router-link>
        <!-- Dark Mode Toggle Button -->
        <button class="dark-mode-toggle" @click="toggleDarkMode">
          {{ darkMode ? "Light Mode" : "Dark Mode" }}
        </button>
      </nav>
    </header>

    <!-- Split Content -->
    <main class="main-content">
      <div class="left-content">
        <h2>Welcome to Crypto Exchange Marketplace</h2>
        <p>Trade cryptocurrencies easily and securely.</p>
      </div>
      <div class="right-content">
        <keep-alive>
          <transition name="fade" mode="out-in">
            <router-view :key="$route.name" />
          </transition>
        </keep-alive>
      </div>
    </main>
  </div>
</template>

<script lang="ts">
import { ref } from "vue";

export default {
  name: "App",
  setup() {
    // Reactive variable for dark mode state
    const darkMode = ref(false);

    // Function to toggle dark mode
    const toggleDarkMode = () => {
      darkMode.value = !darkMode.value;
      console.log("Dark mode toggled:", darkMode.value);
      if (darkMode.value) {
        document.body.classList.add("dark-mode");
      } else {
        document.body.classList.remove("dark-mode");
      }
    };

    // Return variables and methods to the template
    return {
      darkMode,
      toggleDarkMode,
    };
  },
};
</script>

<style scoped>
/* App Container */
.app-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background: linear-gradient(to bottom, #e4f0f6, #ffffff);
  color: #333;
  font-family: Arial, sans-serif;
}

body.dark-mode .app-container {
  background: black;
}

/* Header */
.app-header {
  background-color: #1877f2;
  color: white;
  padding: 10px 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.header-title {
  font-size: 24px;
  font-weight: bold;
}

.navigation a {
  color: white;
  margin: 0 10px;
  text-decoration: none;
  padding: 5px 10px;
  border-radius: 5px;
  transition: background-color 0.3s ease;
}

.navigation a:hover {
  background-color: #155dc9;
}

body.dark-mode .navigation a:hover {
  background-color: #555;
}

/* Main Content */
.main-content {
  margin: 20px;
  display: grid;
  grid-template-columns: auto 1fr;
  grid-template-rows: 1fr;
  gap: 5%;
  margin-top: 30vh;
 
}

body.dark-mode .main-content {
  background-color: #121212;
  color: white;
}

.left-content {
  justify-self: start;
  text-align: center;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

body.dark-mode .left-content {
  background-color: #121212;
  color: white;
}

.left-content h2 {
  font-size: 36px;
  font-weight: bold;
  margin-bottom: 10px;
  color: #333;
}

.left-content p {
  font-size: 25px;
  color: #666;
}

.right-content {
  justify-self: start;
}

/* Dark Mode Toggle Button */
.dark-mode-toggle {
  width: 8%;
  background-color: #3a3a3a;
  color: white;
  border: 1px solid #ccc;
  border-radius: 10px;
  padding: 10px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  position: fixed;
  bottom: 20px;
  right: 20px;
  z-index: 1000;
  box-sizing: border-box;
}

.dark-mode-toggle:hover {
  background-color: #555;
}

/* Fade Transition */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s ease;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
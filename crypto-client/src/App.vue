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
        <transition name="fade" mode="out-in">
          <router-view :key="$route.fullPath" />
        </transition>
      </div>
    </main>
  </div>
</template>

<script>
export default {
  name: "App",
  data() {
    return {
      darkMode: false, // State for dark mode
    };
  },
  methods: {
    toggleDarkMode() {
      this.darkMode = !this.darkMode;
      console.log("Dark mode toggled:", this.darkMode);
      if (this.darkMode) {
        document.body.classList.add("dark-mode");
      } else {
        document.body.classList.remove("dark-mode");
      }
    },
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
  display: flex;
  flex: 1;
  align-items: center;
  justify-content: space-between;
  padding: 2rem;
}

body.dark-mode .main-content {
  background-color: #121212; /* Matches the page background */
  color: white; /* Text color for the main content */
}

.left-content {
  flex: 1;
  padding: 2rem;
  color: #333;
  text-align: center;
}

body.dark-mode .left-content {
  color: white; /* Make the text white */
}

.left-content h2 {
  font-size: 36px; /* Bigger text */
  font-weight: bold;
  margin-bottom: 10px;
  color: #333;
}

.left-content p {
  font-size: 25px; /* Larger subtitle */
  color: #666; /* Subtle color */
}

.right-content {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
}

body.dark-mode .right-content {
  background-color: #1e1e1e; /* Slightly lighter for contrast */
  color: white; /* Text color */
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
  position:fixed ; /* Fix the button to the viewport */
  bottom: 20px; /* Distance from the bottom */
  right: 20px; /* Distance from the right */
  z-index: 1000; /* Ensure it appears above other elements */
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


<template>
  <div :class="$options.name">
    <app-header />
    <app-sidebar />
    <div :class="`MainGrid`">
      <slot />
    </div>
  </div>
</template>

<script>
import AppSidebar from "@/components/core/menu/AppSidebar.vue";
import AppHeader from "@/components/core/AppHeader.vue";

export default {
  name: "LayoutDefault",

  components: {
    AppHeader,
    AppSidebar
  }
};
</script>

<style lang="scss">
@import "@/scss/abstracts/_colors.scss";

.LayoutDefault {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: black;
  height: 100vh;
  display: grid;
  grid-template-columns: 15% 1fr;
  grid-template-rows: 50px 1fr;
  grid-template-areas:
    "sidebar header"
    "sidebar main";
}

.MainGrid {
  grid-area: main;
  padding: 1%;
}
</style>

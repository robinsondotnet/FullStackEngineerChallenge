<template>
  <aside :class="$options.name">
    <div :class="`${$options.name}__brand-imageWrapper`">
      <img
        :class="`${$options.name}__brand-imageWrapper-image`"
        :src="brandUrl"
      />
    </div>

    <div :class="`${$options.name}__avatarWrapper`">
      <base-avatar :image-url="currentUser.avatarUrl" />
      <h3>{{ currentUser.name }}</h3>
    </div>

    <nav :class="`${$options.name}__menuWrapper`">
      <app-sidebar-menu title="Customer" :menu-options="menuOptions" />
    </nav>
  </aside>
</template>

<script>
import BaseAvatar from "@/components/base/BaseAvatar.vue";
import AppSidebarMenu from "./AppSidebarMenu.vue";
import { mapState } from "vuex";

export default {
  name: "AppSidebar",

  components: {
    BaseAvatar,
    AppSidebarMenu
  },

  computed: {
    ...mapState({
      currentUser: state => state.user,
      menuOptions: state => state.permission.menuOptions
    })
  },
  data() {
    return {
      brandUrl:
        "https://www.belatrixsf.com/wp-content/themes/Belatrix/images/logo-belatrix.png"
    };
  }
};
</script>

<style lang="scss">
@import "@/scss/abstracts/_colors.scss";
@import "@/scss/abstracts/_mixins.scss";

.AppSidebar {
  @include bg-transparent($secondary-color);
  grid-area: sidebar;
  color: #fff;
  min-width: 252px;

  &__brand-imageWrapper {
    height: 50px;
    align-items: center;
    justify-content: center;
    display: flex;

    &-image {
      width: 200px;
    }
  }

  &__avatarWrapper {
    background-color: $secondary-color;
    height: 10vh;
    justify-content: space-evenly;
    align-items: center;
    display: flex;
    flex-direction: row;
  }
}
</style>

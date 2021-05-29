<template>
  <div class="container">
      <h2>Anzahl Spieler</h2>
      <div class="circle">{{ numberPlayers }}</div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class NumberPlayers extends Vue {
  private numberPlayers: string = '';

  private async mounted() {
      // Load all Players from Server
      this.loadNumberPlayers();
      setInterval(() => {
          this.loadNumberPlayers();
      }, 5000);
  }
  private async loadNumberPlayers() {
    const response = await axios.get('http://localhost:8888/api/game');
    this.numberPlayers = (response.data.length).toString();
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.circle {
    width:125px;
    height:125px;
    border-radius:80px;
    font-size:25px;
    color:black;
    line-height:125px;
    text-align:center;
    border: 1px solid black;
}
.container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center; 
}
</style>

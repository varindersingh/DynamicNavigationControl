<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>

  <xsl:template match="/NavigationLayout">
    <xsl:apply-templates></xsl:apply-templates>
  </xsl:template>

  <xsl:template match="Section">
    <xsl:if  test="current()/@visible!='false'">
      <h4>
        <xsl:attribute name="class">
          <xsl:text>main-nav</xsl:text>
          <xsl:if test="/*/@expandedSectionID=@ID">
            <xsl:text> open</xsl:text>
          </xsl:if>
        </xsl:attribute>
        <xsl:if test="string-length(@title)>0">
          <xsl:value-of select="current()/@title"/>
        </xsl:if>
      </h4>
      <ul>
        <!--<xsl:if  test="current()/@visible!='false'">-->
          <xsl:apply-templates select="*"></xsl:apply-templates>
        <!--</xsl:if>-->
      </ul>
    </xsl:if>
  </xsl:template>

  <xsl:template match="ExpandableSectionsGroup">

    <div data-collapse="accordion">

      <xsl:apply-templates select="*"></xsl:apply-templates>

    </div>

  </xsl:template>


  <xsl:template match="Separator[@visible!='false']">

    <li class="spacer"></li>

  </xsl:template>


  <xsl:template match="Label[@visible!='false']">

    <li class="main-nav">
      <!--<span>
          <xsl:attribute name="class">            
            <xsl:value-of select="@icon-class"/>
          </xsl:attribute>
        </span>-->
      <xsl:value-of select="current()/@title"/>
    </li>
  </xsl:template>



  <xsl:template match="NavItem[not(@visible = 'false')]">
    <li>
      <xsl:if test="@active='true'">
        <xsl:attribute name="class">
          <xsl:text>active</xsl:text>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="@tooltip">
        <xsl:attribute name="title">
          <xsl:value-of select="@tooltip"/>
        </xsl:attribute>
      </xsl:if>
      <a>
        <xsl:if test="current()/@href!=''">
          <xsl:attribute name="href">
            <xsl:value-of select="current()/@href" />
          </xsl:attribute>
        </xsl:if>
        <span>
          <xsl:attribute name="class">
            <xsl:text>ico </xsl:text>
            <xsl:value-of select="@icon-class"/>
          </xsl:attribute>
        </span>
        
        <xsl:value-of  select="current()/@title" />
        
        <xsl:if test="current()/@count>0">
          <!--<span id="spnExtCount" class="count active">
            <xsl:value-of select="current()/@count" />
          </span>-->

          <em class=" alert-msg ml5">
            <xsl:value-of select="current()/@count" />          
          </em>    
        </xsl:if>
       
      </a>
    </li>
  </xsl:template>

</xsl:stylesheet>
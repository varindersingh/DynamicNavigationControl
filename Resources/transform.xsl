<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>

  <xsl:param name="persistState"></xsl:param>
  <xsl:param name="accordion"></xsl:param>
  <xsl:param name="staticHost"></xsl:param>
  <xsl:param name="isDevMode"></xsl:param>
  
  <xsl:template match="/NavigationLayout">
    
    
    <xsl:apply-templates></xsl:apply-templates>
    
    
    <script type="text/javascript">
                 
      jQuery(document).ready(function(){
                       
        try{ 
        if(typeof jQuery.fn.collapse != 'function')
        console.warn("NavigationControl: jquery.collapse js is not loaded.Please make sure both Jquery and jquery.collapse are placed before navigation control loads.");
        }catch(er){}
        
        jQuery("#nav-expandable-sections").collapse({
        open: function() {
        this.slideDown(400);
        },
        close: function() {
        this.slideUp(400);
        },
        accordion: <xsl:value-of select="$accordion" ></xsl:value-of>,
        persist: <xsl:value-of select="$persistState" ></xsl:value-of>
        });
    });
    </script>

  </xsl:template>

  <xsl:template match="Section">
    <xsl:if  test="current()/@visible!='false'">
      <xsl:element name="h4">
        <xsl:choose>
          <xsl:when test="string-length(@title)>0">
            <xsl:attribute name="class">
              <xsl:text>main-nav </xsl:text>
              <xsl:if test="/*/@expandedSectionID=@ID">
                <xsl:text> open</xsl:text>
              </xsl:if>
            </xsl:attribute>
            <xsl:value-of select="current()/@title"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:attribute name="class">
              <xsl:text>main-nav-nob</xsl:text>
            </xsl:attribute>
          </xsl:otherwise>
        </xsl:choose>                
      </xsl:element>           
      <ul>
        <xsl:apply-templates select="NavItem"></xsl:apply-templates>
      </ul>
       <xsl:if test="string-length(@title)>0">
        <xsl:apply-templates select="Callout[@visible!='false']"></xsl:apply-templates>
      </xsl:if>
    </xsl:if>
  </xsl:template>


  <xsl:template match="Callout[@visible!='false']">

    <div class="tooltip" style="position: absolute; left: 160px; top: -10px;display: block;z-index:101;">
      <div class="pad">
        <div class="popupdv">
          <img src="//{$staticHost}/wiziqcss/css/skin01/images/tooltip_arrow.png" alt="" />
        </div>
        <p>
          <strong>
            <xsl:value-of select="@title"/>
          </strong>
        </p>
        <p class="martop5 lightext11 fleft" runat="server" id="pClassToolTip">
          <xsl:value-of select="text()"/>
        </p>
      </div>
      <p class="mt10" style="background: #F5F5F5; padding: 0 0 6px 0">
        <span class="fright mr10">
          <a href="javascript:void(0);" class="s_boldbuttons" style="font-weight:normal">
            <span data-control-name="">
              <xsl:value-of select="concat(@buttonText,substring('Ok', 1 + 2*boolean(@buttonText)))"/>
            </span>
          </a>
        </span>
      </p>
    </div>

  </xsl:template>

  <xsl:template match="ExpandableSectionsGroup">
    <div id="nav-expandable-sections" >
      <xsl:apply-templates select="*">
        <xsl:sort data-type="number" order="ascending" select="concat(@displayIndex,substring('9999', 1 + 2*boolean(@displayIndex)))"/>
      </xsl:apply-templates>
    </div>
  </xsl:template>

 
  
  <xsl:template match="Separator[@visible!='false']">

    <li class="spacer"></li>

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
          <em class=" alert-msg ml5">
            <xsl:value-of select="current()/@count" />
          </em>
        </xsl:if>

      </a>
    </li>
  </xsl:template>

</xsl:stylesheet>
